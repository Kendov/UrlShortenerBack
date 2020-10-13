using System.Collections.Generic;
using MongoDB.Driver;
using urlShortener.Data;
using urlShortener.Models;

using System.Net.Http;
using AutoMapper;
using System;
using urlShortener.Services.DomainNotification;

namespace urlShortener.Services
{
    public class UrlDataService : IUrlDataService
    {
        private readonly MongoDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationContext _notification;
        public UrlDataService(
            MongoDbContext urlDataCollection,
            IMapper mapper,
            INotificationContext notification
        )
        {
            _context = urlDataCollection;
            _mapper = mapper;
            _notification = notification;
        }
        public UrlData Get(string Id)
        {
            return _context.UrlData
                .Find(x => x.Id == Id)
                .FirstOrDefault();
        }

        public string GetUrl(string Id)
        {
            return _context.UrlData
                .Find(x => x.Id == Id)
                .FirstOrDefault()
                .Url;
        }

        public List<UrlData> List()
        {
            return _context.UrlData
                .Find(x => true)
                .ToList();

        }

        public UrlData Post(UrlDataEntryModel entryValue)
        {
            if (!Uri.IsWellFormedUriString(entryValue.Url, UriKind.Absolute))
            {
                //throw new HttpRequestException("Bad url");
                
                _notification.AddNotification("Bad url - The url is not valid");
                return null;
            }

            UrlData entry = _mapper.Map<UrlData>(entryValue);

            // if no id -> generate a random one
            // if id -> check if exist before insert
            if (string.IsNullOrEmpty(entry.Id))
            {
                entry.Id = IdGenerator.GenerateId();
                _context.UrlData.InsertOne(entry);
                return entry;
            }

            if (_context.UrlData.Find(x => x.Id == entry.Id).Any())
            {
                _notification.AddNotification("Id already exist");
                return null;
            }
            _context.UrlData.InsertOne(entry);
            return entry;
        }
    }
}