using Lab2B.Service;
using Lab2B.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2B.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService commentService;
        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        /// <summary>
        /// Gets all the movies
        /// </summary>
        /// <returns></returns>
        // GET: api/Flowers
        [HttpGet]
        public PaginatedList<CommentGetModel> Get([FromQuery]string filterString, [FromQuery]int page = 1)
        {
            page = Math.Max(page, 1);
            return commentService.GetAll(page, filterString);
        }
    }
}
