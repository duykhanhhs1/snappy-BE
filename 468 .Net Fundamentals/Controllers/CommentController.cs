using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _468_.Net_Fundamentals.Controllers
{
    [Route("api/comments")]
    [ApiController]
    [Authorize]
    /*[Authorize(Roles = AppUserRole.Admin)]*/
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost("/api/card/{cardId}/comments")]
        public async Task Create(int cardId, [FromBody] CommentCreateVM comment)
        {
            await _commentService.Create(cardId, comment);
        }

    }
}
