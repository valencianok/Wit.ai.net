﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.valgut.libs.bots.Wit.Models
{
    public class Message : WitApiResponse
    {
        /// <summary>
        /// Either the ID you passed to the API or an ID generated by the API
        /// </summary>
        public string msg_id { get; set; }

        /// <summary>
        /// Either the text sent in the q argument or the transcript of the speech input. This value should be used only for debug as Wit.ai focuses on entities.
        /// </summary>
        public string _text { get; set; }

        /// <summary>
        /// Object of entities. Each entity will contain an array [] of values even if there is only one value.
        /// </summary>
        public Dictionary<string, List<Entity>> entities { get; set; }
    }
}
