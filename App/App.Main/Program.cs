using App.Domain.Features.Places;
using App.Domain.Interfaces.Places;
using App.Domain.Models;
using App.Infrastructure.Clients;
using App.Infrastructure.Helpers;
using App.Infrastructure.Scraping;
using App.Main.IoC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace App.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = SimpleInjectorContainer.Initialize();

            IVenueService venueService = container.GetInstance<IVenueService>();
            ILocationService locationService = container.GetInstance<ILocationService>();
            InstagramScraper bot = container.GetInstance<InstagramScraper>();

            bot.Initialize();
            bot.Login();
            var venues = venueService.GetAll();
            var exceptionMessages = new List<string>();
            foreach (var venue in venues)
            {
                try
                {
                    venue.Location = locationService.GetByVenueId(venue.Id);
                    bot.FindVisitorByVenue(venue);
                    venueService.InsertVisitors(venue);
                }
                catch (Exception ex)
                {
                    exceptionMessages.Add(string.Format("Mensage: {0} --> InnerException: {1} --> VenueId: {2} --> VenueName: {3}", ex.Message, ex.InnerException, venue.Id, venue.Name));
                }
            }
            System.IO.File.WriteAllLines(ConfigurationManager.AppSettings.Get("FilesPath"), exceptionMessages);
            bot.Close();
        }

        //Os fluxos abaixo foram executados na ordem em que se apresentam

        //Step 1 
        //Busca uma lista de coordenadas no arquivo KML
        //Para cada coordenada busca dados de lugares na api do Foursquare (Parametro true para salvar respostas das consultas no banco de dados, ou nada, para não salvar)
        //Converte o resultado da consulta em um objeto do sistema, verifica se ele já existe no banco e insere (ou não) no banco de dados
        private static void RequestAndStorePlaces(IVenueService venueService)
        {
            IList<string> coordinates = KMLReader.GetCoordiates();
            foreach (var latLon in coordinates)
            {
                RawVanuesData rawVenues = FoursquareClient.GetVenues(latLon, true);
                foreach (var rawVenue in rawVenues.Response.Venues)
                    venueService.Insert(new Venue(rawVenue));
            }
        }

        //Step 2
        //Busca todos os lugares no banco de dados
        //Compara os nomes de todos os lugares entre si para buscar similaridades (distancia de Levenshtein normalizada)
        //Insere no banco o resultado do calculo se for menor que 0.2
        private static void RemoveSimilarVenues(IVenueService venueService, ILocationService locationService)
        {
            List<Venue> venuesToRemove = new List<Venue>();
            List<Venue> venues = venueService.GetAll().ToList();
            for (int i = 0; i < venues.Count - 1; i++)
            {
                int nextIndex = i + 1;
                venues[i].SimilarVenues = venues.GetRange(nextIndex, venues.Count - nextIndex)
                                                .Select(x => new VenueSimilarityDTO
                                                {
                                                    Venue = new Venue
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name
                                                    }
                                                })
                                                .ToList();
                venues[i].CalculateNameSimilarity();
                foreach (var similar in venues[i].SimilarVenues)
                {
                    if (similar.Similarity <= (decimal)0.2)
                    {
                        venues[i].Location = locationService.GetByVenueId(venues[i].Id);
                        similar.Venue.Location = locationService.GetByVenueId(similar.Venue.Id);
                        if (venues[i].Location.CalculateDistance(similar.Venue.Location) < 100)
                            venuesToRemove.Add(similar.Venue);
                    }
                }
            }
            foreach (var venue in venuesToRemove.Distinct())
            {
                Console.WriteLine(venue.Name);
                venueService.DeleteById(venue);
            }
        }
    }
}
