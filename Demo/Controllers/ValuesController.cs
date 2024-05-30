using Demo.Models;
using Demo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly context ct;
        public ValuesController(context ct)
        {
            this.ct = ct;
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<Emp> GetUsers() { 
            return ct.emps.ToList();
        }
        [HttpGet]
        [Route("GetUser")]
        public Emp GetUser(int id)
        {
            return ct.emps.Where(x => x.id == id).FirstOrDefault();
        }

        [HttpPost]
        [Route("AddUser")]
        public string AddUser(Emp emp)
        {
            ct.emps.Add(emp);
            ct.SaveChanges();
            return "User Added";
        }
        [HttpPost]
        [Route("UpdateUSer")]
        public string UpdateUser(Emp emp)
        {
            ct.emps.Update(emp);
            ct.SaveChanges();
            return "User Updated";
        }
        [HttpDelete]
        [Route("DeleteUSer")]
        public string DeleteUser(int id)
        {
            Emp emp = ct.emps.Where(x=>x.id==id).FirstOrDefault();
            if (emp != null)
            {
                ct.emps.Remove(emp);
                ct.SaveChanges();
                return "User Deleted";
            }
            else return "No User Deleted";
        }

    }
}
