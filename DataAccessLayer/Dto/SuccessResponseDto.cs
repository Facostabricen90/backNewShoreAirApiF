using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dto
{
    public class SuccessResponseDto
    {
        public string? departureStation { get; set; }
        public string? arrivalStation { get; set; }
        public string? flightCarrier { get; set; }
        public string? flightNumber { get; set; }
        public double price { get; set; }
    }
}
