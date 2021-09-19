using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PayManWebAPI.DBContexts;
using PayManWebAPI.Models;

namespace PayManWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private MyDBContext myDbContext;

        public TodoItemController(MyDBContext context)
        {
            myDbContext = context;
        }

        [HttpGet]
        public IList<TodoItem> Get()
        {
            return (this.myDbContext.TodoItems.ToList());
        }
    }
}
