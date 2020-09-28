using System.Collections.Generic;
using MongoDB.Driver;
using urlShortener.Data;
using urlShortener.Models;
using shortid;
using shortid.Configuration;
using System;
using System.Net.Http;

namespace urlShortener.Services
{
    public class UrlDataService : IUrlDataService
    {
        private readonly MongoDbContext _context;
        public UrlDataService(MongoDbContext urlDataCollection)
        {
            _context = urlDataCollection;
            
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

        public UrlData Post(UrlData entry)
        {
            // if no id -> generate a random one
            // if id -> check if exist before insert
            if(string.IsNullOrEmpty(entry.Id))
            {
                entry.Id = IdGenerator.GenerateId();
                _context.UrlData.InsertOne(entry);
                return entry;
            }
            
            if(_context.UrlData.Find(x => x.Id == entry.Id).Any())
            {
                throw new HttpRequestException("Id already exist");
            }
            _context.UrlData.InsertOne(entry);
            return entry;
        }
    }
}