using System;
using System.Collections.Generic;

namespace Api.Domain.Presenters
{
    public class LikePresenter
    {
        public string Id { get; set; }

        public string IpUsr { get; set; }

        public Guid? IdeiaId { get; set; }

        public Guid? ProblemaId { get; set; }
    }
}
