﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Google.Apis.Services;
using SpotifyListner.Web.Models;
using System.Linq;
using System.Xml;
using Google.Apis.YouTube.v3;

namespace SpotifyListner.Web.Services
{
    public class YouTubeGoogleService : IYouTubeGoogleService
    {
        private YouTubeService youtubeService;

        public IKeyService m_keyService { get; }

        public YouTubeGoogleService(IKeyService keyService)
        {
            m_keyService = keyService;
        }

        public async Task<string> FetchUrl(SpotifyContent spotifySong, string mode)
        {
            await CreateYouTubeService();

            var searchListRequest = youtubeService.Search.List("snippet");
            var searchTerm = spotifySong.item.name + " " + spotifySong.item.artists.First().name;
            if (!string.IsNullOrEmpty(mode))
            {
                searchTerm += " " + mode; 
            }
            searchListRequest.Q = searchTerm; 
            searchListRequest.MaxResults = 1;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var song = searchListResponse.Items.FirstOrDefault(s => s.Id.Kind.Equals("youtube#video"));
            var songId = song?.Id?.VideoId;
            if (song == null)
            {
                //TODO: pick random videos if not found.
                return "15_Y3_eRfOU&t";
            }

            return songId;
        }

        public async Task<List<SpotifySearchResult>> GetSearchResults(SpotifyContent spotifySong, string mode, int maxResults)
        {
            await CreateYouTubeService();

            var searchListRequest = youtubeService.Search.List("snippet");
            var searchTerm = spotifySong.item.name + " " + spotifySong.item.artists.First().name;
            if (!string.IsNullOrEmpty(mode))
            {
                searchTerm += " " + mode;
            }
            searchListRequest.Q = searchTerm;
            searchListRequest.MaxResults = maxResults;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var songs = searchListResponse.Items.Where(s => s.Id.Kind.Equals("youtube#video"));
            var spotifySearchResults = new List<SpotifySearchResult>();
            foreach (var song in songs)
            {
                spotifySearchResults.Add(new SpotifySearchResult
                {
                    Name = song.Snippet.Title,
                    ThumbnailUrl = song.Snippet.Thumbnails.Default__.Url,
                    VideoId = song.Id.VideoId

                });
            }

            return spotifySearchResults;
        }

        public async Task<TimeSpan> GetSongLength(string id)
        {
            await CreateYouTubeService();

            var videoRequest = youtubeService.Videos.List("id, contentDetails");
            videoRequest.Id = id;
            var video = await videoRequest.ExecuteAsync();
            var videoDuration = XmlConvert.ToTimeSpan(video.Items.FirstOrDefault()?.ContentDetails.Duration);
            return videoDuration;
        }

        private async Task CreateYouTubeService()
        {
            if (youtubeService == null)
            {
                var keys = await m_keyService.GetKeys();

                youtubeService = new YouTubeService(new BaseClientService.Initializer
                {
                    ApiKey = keys.YouTubeServiceId, // "YouTube-APIkey => https://console.developers.google.com/apis/credentials/",
                    ApplicationName = "PartyfyYou"
                });

            }

        }
    }
}