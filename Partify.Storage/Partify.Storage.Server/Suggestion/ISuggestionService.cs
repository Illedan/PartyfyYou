﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Suggestion
{
    public interface ISuggestionService
    {
        Task PostSuggestion(CreateSuggestionRequest request);
        //Task PostSuggestionRelation(CreateSuggestionRelationRequest request);
    }
}
