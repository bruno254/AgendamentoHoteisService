﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AgendamentoHoteis.Business.Models
{
    public abstract class Entity
    {
        [Key]
        public long Id { get; set; }
    }
}
