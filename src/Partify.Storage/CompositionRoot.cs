using LightInject;
using Partify.Storage.Query;
using Partify.Storage.Song;
using Partify.Storage.Suggestion;
using Partify.Storage.Video;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register(factory => factory);
            serviceRegistry.Register<IQueryExecutor, QueryExecutor>();

            // SpotifySong
            serviceRegistry.Register<ISongService, SongService>();
            serviceRegistry.Register<IQueryHandler<SongQuery, SongResult>, SongQueryHandler>();

            // YoutubeVideo
            serviceRegistry.Register<IVideoService, VideoService>();
            serviceRegistry.Register<IQueryHandler<VideoQuery, VideoResult>, VideoQueryHandler>();

            // Suggestion
            serviceRegistry.Register<ISuggestionService, SuggestionService>();
            serviceRegistry.Register<IQueryHandler<SuggestionQuery, SuggestionResult>, SuggestionQueryHandler>();
        }
    }
}
