using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Partify.Storage.Server.UseCase;
using Partify.Storage.Server.Suggestion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Partify.Storage.WebApi.Controller
{
    [Route("api/[controller]")]
    public class Suggestion : ControllerBase
    {
        private readonly ISuggestionHandlerService m_suggestionHandlerService;
        public Suggestion(ISuggestionHandlerService suggestionHandlerService)
        {
            m_suggestionHandlerService = suggestionHandlerService;
        }

        // GET: api/values
        [HttpGet]
        [ProducesResponseType(typeof(SuggestionRelationResult), 200)]
        public async Task<SuggestionRelationResult> Get(string songId, Guid modeId, Guid userId)
        {
            var result = await m_suggestionHandlerService.GetSuggestion(new SuggestionRelationRequest {ModeId = modeId, UserId = userId, SongId = songId});
            return result;
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]PostSuggestionModel postSuggestionModel)
        {
            await m_suggestionHandlerService.AddSuggestion(postSuggestionModel.VideoId, postSuggestionModel.SongId, postSuggestionModel.ModeId, postSuggestionModel.UserId);
        }

        // DELETE api/values/5
        [HttpDelete("{userSuggestionId}")]
        public void Delete(Guid userSuggestionId)
        {
            m_suggestionHandlerService.RemoveSuggestion(userSuggestionId);
        }
    }
}
