﻿using System;
using NUnit.Framework;
using OrcaMDF.Framework;
using OrcaMDF.RawCore.Records;
using OrcaMDF.RawCore.Utilities.SQL2012;
using System.Linq;

namespace OrcaMDF.RawCore.Tests.Utilities.SQL2012
{
	public class SQL2012BaseSchemaTests : BaseFixture
	{
		[Test]
		public void sysschobjs()
		{
			var db = new RawDataFile(AW2012Path);
			var pages = db.Pages.Where(x => x.Header.ObjectID == SQL2012SystemObjects.sysschobjs.ObjectID && x.Header.Type == PageType.Data);
			var records = pages.SelectMany(x => x.Records).Cast<RawPrimaryRecord>();
			var rows = RawColumnParser.Parse(records, SQL2012SystemObjects.sysschobjs.Schema).ToList();

			Assert.AreEqual(2291, rows.Count());

			var testRow = rows.Single(x => (int)x["id"] == -93184835);
			Assert.AreEqual("sp_helpsrvrolemember", testRow["name"]);
			Assert.AreEqual(Convert.ToDateTime("2011-11-04 21:12:23.257"), testRow["created"]);
		}
	}
}