﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Spravochniki
{
    public static class Spravochniks
    {
        public static List<string> SprPlotCodes = new List<string>
        {

        };
        public static List<string> SprRecieverTypeCode = new List<string>
        {
            "101","201","211","301","311","401","411","501","601","611","811","821","831","911","921","991","102","202","212","302","312","402","412","502","602","612","812","822","832","912","922","992","109","209","219","309","319","409","419","509","609","619","819","829","839","919","929","999"
        };
        public static List<string> SprRifineOrSortCodes = new List<string>
        {
            "11","12","13","14","15","16","17","19","21","22","23","24","29","31","32","39","41","42","43","49","51","52","53","54","55","61","62","63","71","72","73","74","79","99"
        };
        public static List<Tuple<string, long, long>> SprRadionuclids
        {
            get
            {
                return SprRadionuclidsTask.Result;
            }
            private set
            {

            }
        }
#if DEBUG
        private static Task<List<Tuple<string, long, long>>> SprRadionuclidsTask = ReadCsvAsync(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"))+ "data\\Spravochniki\\RadionuclidsActivities.csv");
#else
        private static Task<List<Tuple<string, long, long>>> SprRadionuclidsTask = ReadCsvAsync(Path.GetFullPath(AppContext.BaseDirectory)+"data\\Spravochniki\\RadionuclidsActivities.csv");
#endif

        private static Task<List<Tuple<string, long, long>>> ReadCsvAsync(string path)
        {
            return Task.Run(() => ReadCsv(path));
        }

        private static List<Tuple<string, long, long>> ReadCsv(string path)
        {
            var res = new List<Tuple<string, long, long>>();
            string[] radionuclids = File.ReadAllLines(path);
            for (int k = 1; k < radionuclids.Count(); k++)
            {
                var tmp = radionuclids[k].Split(";");
                string i1 = tmp[0];
                long i2 = long.Parse(tmp[1]);
                long i3 = long.Parse(tmp[2]);
                res.Add(new Tuple<string, long, long>(i1, i2, i3));
            }
            return res;
        }
    }
}
