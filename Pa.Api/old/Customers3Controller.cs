﻿//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Para.Data.Context;
//using Para.Data.Domain;

//namespace Pa.Api.old
//{
//    [NonController]
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Customers3Controller : ControllerBase
//    {
//        private readonly ParaDbContext dbContext;
//        public Customers3Controller(ParaDbContext dbContext)
//        {
//            this.dbContext = dbContext;
//        }

//        // GET: api/<Customers>
//        [HttpGet]
//        public async Task<List<Customer>> Get()
//        {
//            var entityList = await dbContext.Set<Customer>().ToListAsync();
//            return entityList;
//        }

//        // GET api/<Customers>/5
//        [HttpGet("{id}")]
//        public async Task<Customer> Get(long customerId)
//        {
//            var entity1 = await dbContext.Set<Customer>().Include(x => x.CustomerAddresses).Include(x => x.CustomerPhones).Include(x => x.CustomerDetail).ThenInclude(x => x.Customer).FirstOrDefaultAsync(x => x.Id == customerId);
//            var entity2 = await dbContext.Customers.Include(x => x.CustomerAddresses).Include(x => x.CustomerPhones).Include(x => x.CustomerDetail).ThenInclude(x => x.Customer).FirstOrDefaultAsync(x => x.Id == customerId);
//            return entity1;
//        }

//        // POST api/<Customers>
//        [HttpPost]
//        public async Task Post([FromBody] Customer value)
//        {
//            var entity = await dbContext.Set<Customer>().AddAsync(value);
//            await dbContext.SaveChangesAsync();
//        }

//        // PUT api/<Customers>/5
//        [HttpPut("{customerId}")]
//        public async Task Put(long customerId, [FromBody] Customer value)
//        {
//            dbContext.Set<Customer>().Update(value);
//            await dbContext.SaveChangesAsync();
//        }

//        // DELETE api/<Customers>/5
//        [HttpDelete("{customerId}")]
//        public async void Delete(long customerId)
//        {
//            var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == customerId);
//            dbContext.Set<Customer>().Remove(entity);
//            await dbContext.SaveChangesAsync();

//        }
//    }
//}
