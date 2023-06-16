using Flights.Details;
using Flights.Models;
using Flights.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Identity.Client;


namespace Flights.Repository
{
    public interface IMethods //CRUD operations to Database
    {
        bool Addmethod(FlightData p);
        bool Updatemethod(UpdateEmployeeViewModel p);
        bool Deletemethod(Guid id);
        List<FlightData> GetAllmethod();
        FlightData Getmethod(Guid id);

        bool DeleteAllmethod();
        List<FlightData> search(string name);

        int Countdepartureflights();
        int CountArrivalflights();
        string Departfirst();
        string arrivefirst();

    }
    public class crudmethods : IMethods
    {
        private readonly Flightdetailsdbcontext context;
        public crudmethods(Flightdetailsdbcontext _context)
        {
            this.context=_context;
        }
        public bool Addmethod(FlightData p)  //Add method
        {
            try
            {
                context.FlightDatas.Add(p);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAllmethod()
        {
            try
            {
                var p = context.FlightDatas.ToList();
                foreach(var x in p)
                {
                    context.FlightDatas.Remove(x);
                    context.SaveChanges();
                }
              
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Deletemethod(Guid id) //Delete method
        {
            var p=context.FlightDatas.FirstOrDefault(x => x.id == id);
            if (p != null)
            {
                context.FlightDatas.Remove(p);
                context.SaveChanges();
                return true;
              
            }
            return false;
        }

        public List<FlightData> GetAllmethod() //Get all data from sql
        {
            return context.FlightDatas.ToList();
        }

        public FlightData Getmethod(Guid id) //Get data for the id
        {
            var m=context.FlightDatas.FirstOrDefault(x => x.id == id);
            if (m!=null)
            {
                FlightData k = new FlightData();
                k.id = m.id;
                k.flightid = m.flightid;
                k.departure_destination = m.departure_destination;
                k.arrival_destination = m.arrival_destination;
                k.arrival_date = m.arrival_date;
                k.departure_date = m.departure_date;
                return k;
            }
            else
            {
                return null;
            }
        }

       

        public bool Updatemethod(UpdateEmployeeViewModel p) //method for editing
        {
            var k= context.FlightDatas.FirstOrDefault(x => x.id == p.id);
            if (k!=null)
            {
                k.id = p.id;
                k.flightid = p.flightid;
                k.departure_destination = p.departure_destination;
                k.arrival_destination = p.arrival_destination;
                k.arrival_date = p.arrival_date;
                k.departure_date = p.departure_date;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

       public List<FlightData> search(string name)//method for searching
        {
            var p =context.FlightDatas.Where(f => (f.flightid.StartsWith(name) || f.arrival_destination.StartsWith(name) || f.departure_destination.StartsWith(name))).ToList();
            return p;
        }

        public int Countdepartureflights()
        {
            int count = 0;
           foreach (var f in context.FlightDatas)
            {
                DateTime k = Convert.ToDateTime(f.departure_date);
                TimeSpan duration = k.Subtract(DateTime.Now);
                if (duration.TotalMinutes < 60 && (f.departure_date>DateTime.Now))
                {
                    count++;
                }
            }
           return count;
        }

        public int CountArrivalflights()
        {
            int count = 0;
            foreach (var f in context.FlightDatas)
            {
                DateTime k = Convert.ToDateTime(f.arrival_date);
                TimeSpan duration = k.Subtract(DateTime.Now);
                if (duration.TotalMinutes < 60 && duration.TotalMinutes>0 && (f.arrival_date > DateTime.Now))
                {
                    count++;
                }
            }
            return count;
        }

        public string Departfirst()
        {
            string depart = null;
            
            double max = 10080;
            foreach(var f in context.FlightDatas) { 
              DateTime k=Convert.ToDateTime(f.departure_date);
                TimeSpan duration = k.Subtract(DateTime.Now);
                if(f.departure_date> DateTime.Now) { 
                   if(duration.TotalMinutes < max)
                    {
                        depart = f.flightid;
                        max = duration.TotalMinutes;
                        
                    }
                }
              

            }
            return depart;
        }

        public string arrivefirst()
        {
            string arrive = null;
            
            double min =10080;
            foreach (var f in context.FlightDatas)
            {
                DateTime k = Convert.ToDateTime(f.arrival_date);
                TimeSpan duration = k.Subtract(DateTime.Now);
                if(f.arrival_date>DateTime.Now)
                {
                    if (duration.TotalMinutes < min)
                    {
                        arrive = f.flightid;
                        min = duration.TotalMinutes;
                        
                    }
                }
               

            }
            return arrive;

        }
    }
}
