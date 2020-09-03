using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lottery.Entities
{
    public class Item
    {
        public Item()
        {
            ItemId = Guid.NewGuid().ToString();
        }
        
        [Key]
        public string ItemId { get; set; }

        /// <summary>
        /// 清單名稱
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 此清單參與者
        /// </summary>
        public ICollection<Attendee> Attendees { get; set; }
        
        /// <summary>
        /// 此清單中獎者
        /// </summary>
        public ICollection<Winner> Winners { get; set; }
    }
}