using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingWebApi.Models
{
    public class Result<T>
    {
        public bool status;
        public string info;
        public T value;
    }
}