using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace ElevenNote.WebMVC.Controllers.WebApi
{
    [Authorize]
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        private bool SetStarState( int noteId, bool newState)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());

            var service = new NoteService(userId);

            var detail = service.GetNoteById(noteId);

            var updatedNote =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content,
                    IsStarred = newState
                };
            return service.UpdateNote(updatedNote);
        }

        [Route("{id}/Star")]
        [HttpPost]
        public bool ToggleStarOn(int id) => SetStarState(id, true);
    }
}