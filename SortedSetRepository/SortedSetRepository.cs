using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    /* I thought about using B+tree, but it's not implemented in c# and this task was not to implement a data structure, wasn't it?
     * So I decided to use SortedSet because it's implemented in .net as a Red Black Tree. 
     * Lookup time for b+tree and reb-black tree is similiar O(logn). 
     * The main advantage of b+tree is that getting all records is easier and faster.
     * 
     * This solution isn't concurrent safe.
     * SortedSet isn't concurrent safe collection. ConcurrentDictionary is implented in .net as an array. So adding/removing entities extends/shrinks the table, which is very expensive. 
     * I hope that this task wasn't about creating concurrent collection.
     * 
     * I decided to add update method. It's faster to update entity data instead of removing and adding node to self-balanced tree. SampleEntity contains only one field so there is no need to wrap it in a transaction.
     */
    public class SortedSetRepository<T> : IRepository<T> where T : IStoreable
    {
        SortedSet<T> _repository = new SortedSet<T>();

        public IEnumerable<T> All()
        {
            return _repository.Select(e => { return (T)e.Clone(); });
        }

        public void Delete(IComparable id)
        {
            _repository.Remove(FindById(id));
        }

        public void Save(T item)
        {
            IStoreable oldItem = _repository.SingleOrDefault(e => e.Id.Equals(item.Id));
            if (oldItem!=null)
                 oldItem.Update(item);
            else
                _repository.Add(item);
        }

        public T FindById(IComparable id)
        {
            T entity = _repository.SingleOrDefault(e => e.Id.Equals(id));
            if(entity==null)
                throw new EntityNotFoundException();

            return (T)entity.Clone();
        }
    }
}
