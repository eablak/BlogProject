using BlogProject.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Model.Entities.Abstract
{
    public abstract class BaseEntity
    {

        public int Id { get; set; } 

        private DateTime _createDate = DateTime.Now;

        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate= value; }
        }

        public DateTime? ModifiedDate { get; set; }  // ? = nullable bir alan veritabanı tarafıında

        public DateTime? RemovedDate { get; set; }

        private Statu _statu = Statu.Active;

        public Statu Statu
        {
            get { return _statu; }
            set { _statu = value; }
        }





    }
}
