using BenchmarkDotNet.Attributes;
using database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efcorevsado
{
    [MemoryDiagnoser]
    public class Benchmarks
    {
        //[Benchmark]
        //[ArgumentsSource(nameof(SearchData))]
        //public async Task EntityFrameworkSearch(List<long> lst)
        //{
        //    var da = new DataAccess();
        //    await da.GetFromEf(lst);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(SearchData))]
        //public async Task AdoSearch(List<long> lst)
        //{
        //    var da = new DataAccess();
        //    await da.GetFromAdo(lst);
        //}

        //public IEnumerable<object> SearchData()
        //{
        //    yield return Enumerable.Range(0, 1000).Select(x => Convert.ToInt64(x)).ToList();
        //    yield return Enumerable.Range(1000, 1000).Select(x => Convert.ToInt64(x)).ToList();
        //    yield return Enumerable.Range(2000, 1000).Select(x => Convert.ToInt64(x)).ToList();
        //    yield return Enumerable.Range(3000, 1000).Select(x => Convert.ToInt64(x)).ToList();
        //    yield return Enumerable.Range(4000, 1000).Select(x => Convert.ToInt64(x)).ToList();
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(InsertData))]
        //public async Task EntityFrameworkInsert(string name)
        //{
        //    var da = new DataAccess();
        //    await da.InsertEf(name);
        //}

        //[Benchmark]
        //[ArgumentsSource(nameof(InsertData))]
        //public async Task AdoFrameworkInsert(string name)
        //{
        //    var da = new DataAccess();
        //    await da.InsertAdo(name);
        //}

        //public IEnumerable<object> InsertData()
        //{
        //    yield return "test1";
        //    yield return "test2";
        //    yield return "test3";
        //    yield return "test4";
        //}

        [Benchmark]
        [ArgumentsSource(nameof(InsertMultipleData))]
        public async Task EntityFrameworkInsertMany(List<string> names)
        {
            var da = new DataAccess();
            await da.InsertManyEf(names);
        }

        [Benchmark]
        [ArgumentsSource(nameof(InsertMultipleData))]
        public async Task AdoInsertMany(List<string> names)
        {
            var da = new DataAccess();
            await da.InsertManyAdo(names);
        }

        public IEnumerable<object> InsertMultipleData()
        {
            yield return Enumerable.Range(0, 1000).Select(x => x.ToString()).ToList();
            yield return Enumerable.Range(1000, 1000).Select(x => x.ToString()).ToList();
            yield return Enumerable.Range(2000, 1000).Select(x => x.ToString()).ToList();
            yield return Enumerable.Range(3000, 1000).Select(x => x.ToString()).ToList();
            yield return Enumerable.Range(4000, 1000).Select(x => x.ToString()).ToList();
        }


    }
}
