using System.Diagnostics;
using System.Linq;
using System;
using NUnit.Framework;

namespace Interview
{
    [TestFixture]
    public class Tests
    {
        private IRepository<IStoreable> _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = new SortedSetRepository<IStoreable>();
        }

        [Test]
        public void SaveNotExistingElement()
        {
            AddSampleEntities(1);

            Assert.AreEqual(1, _repository.All().Count());
        }

        [Test]
        public void SaveExistingElement()
        {
            AddSampleEntities(1);

            string newTitle="changed title";
            IStoreable sampleEntity = new SampleEntity(1, newTitle);
            _repository.Save(sampleEntity);

            Assert.AreEqual(1, _repository.All().Count());
            Assert.AreEqual(newTitle, ((SampleEntity)_repository.FindById(1)).Title);
        }

        [Test]
        public void CheckIfFindByIdReturnDeepCopyOfObject()
        {
            AddSampleEntities(1);

            SampleEntity recordFromRepository= (SampleEntity)_repository.FindById(1);
            string newTitle = "changed title";
            recordFromRepository.Title=newTitle;

            SampleEntity recordFromRepositoryAfterChange= (SampleEntity)_repository.FindById(1);

            Assert.AreNotEqual(newTitle, recordFromRepositoryAfterChange.Title);
        }

        [Test]
        public void CheckIfGetAllReturnDeepCopyOfObjects()
        {
            AddSampleEntities(3);

            SampleEntity recordFromRepository = (SampleEntity)_repository.All().First();
            string newTitle = "changed title";
            recordFromRepository.Title = newTitle;

            SampleEntity recordFromRepositoryAfterChange = (SampleEntity)_repository.All().First();

            Assert.AreNotEqual(newTitle, recordFromRepositoryAfterChange.Title);
        }

        [Test]
        public void GetAllElementsWhereEmpty()
        {
            Assert.IsFalse(_repository.All().Any());
        }

        [Test]
        public void GetAllElementsWhereOne()
        {
            AddSampleEntities(1);

            Assert.AreEqual(1,_repository.All().Count());
        }

        [Test]
        public void GetAllElementsWhereMany()
        {
            AddSampleEntities(2);

            Assert.AreEqual(2, _repository.All().Count());
        }

        [Test]
        public void DeleteNotExistingElement()
        {
            Assert.Throws(typeof(EntityNotFoundException), ()=>_repository.Delete(1));
        }

        [Test]
        public void DeleteExistingElementOnlyOneExists()
        {
            AddSampleEntities(1);

            _repository.Delete(1);
            Assert.IsFalse(_repository.All().Any());
        }

        [Test]
        public void DeleteExistingElement()
        {
            AddSampleEntities(2);

            _repository.Delete(1);
            Assert.AreEqual(1,_repository.All().Count());
        }

        [Test]
        public void FindByIdEmptyRepo()
        {
            Assert.Throws(typeof(EntityNotFoundException), () => _repository.FindById(1));
        }

        [Test]
        public void FindByIdNotExistingId()
        {
            AddSampleEntities(1);
            Assert.Throws(typeof(EntityNotFoundException), () => _repository.FindById(2));
        }

        [Test]
        public void FindByIdWrongId()
        {
            AddSampleEntities(1);
            Assert.Throws(typeof(EntityNotFoundException), () => _repository.FindById("One"));
        }

        [Test]
        public void FindById()
        {
            AddSampleEntities(1);
            Assert.AreEqual(1,_repository.FindById(1).Id);
        }

        private void AddSampleEntities(int count)
        {
            for(int i=1; i<=count; ++i)
            {
                IStoreable sampleEntity = new SampleEntity(i, "test"+i);
                _repository.Save(sampleEntity);
            }
        }
    }
}