using Models.Entities;
using Models.Enums;
using System;
using System.Xml.Serialization;

namespace Models.Models
{
    public class ActionType
    {
        public int Id { get; set; }
        public RequestType RequestType { get; set; }
     }
}
