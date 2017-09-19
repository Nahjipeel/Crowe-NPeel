using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowe.Models;

namespace Crowe.Services
{
    public class MessagesService
    {
        private readonly MessagesContext dbContext;
        public MessagesService(MessagesContext dbCtxt)
        {
            this.dbContext = dbCtxt;
        }

        public Messages GetMessage(int id)
        {
            Log.Information("Getting Message {id}", id);

            var requestedMessage = dbContext.Messages.Where(m => m.Id == id ).SingleOrDefault();

            // destructure operator '@' serializes object into JSON and adds the properties to list of things that can be queried by Serilog
            Log.Fatal("Found {@voter}", requestedMessage/*?.LastName*/);

            if (requestedMessage == null)
            {
                Log.Warning("Requested message with {id} returned null", id);
            }

            return requestedMessage;
        }

        public IEnumerable<Messages> GetMessages()
        {
            return dbContext.Messages;
        }

        public async Task<int> SaveMessage(Messages voter)
        {
            dbContext.Messages.Add(voter);
            return await dbContext.SaveChangesAsync();
        }
    }
}
