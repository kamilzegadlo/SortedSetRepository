using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class SampleEntity : IStoreable, IComparable
    {
        public IComparable Id { get; private set; }

        public String Title { get; set; }

        public SampleEntity(IComparable id, String title)
        {
            Id = id;
            Title = title;
        }

        public void Update(IStoreable item)
        {
            Title = ((SampleEntity)item).Title;
        }

        public int CompareTo(object obj)
        {
            return Id.CompareTo(((SampleEntity)obj).Id);
        }

        public Object Clone()
        {
            return new SampleEntity(Id, Title);
        }
    }
}
