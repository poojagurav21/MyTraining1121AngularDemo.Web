﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo.TodoApplication
{
    public class TodoItem : FullAuditedEntity
    {
        public string Text { get; set; }
    }
}
