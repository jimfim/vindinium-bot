using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace vindinium.tests
{
	[TestFixture]
    public class ServerTests
	{
		int size = 12;
		string map = @"################################        ############$-            $-########  @1        @4  ######    []  $-$-  []    ####    ##  ####  ##    ####    ##  ####  ##    ####    []  $-$-  []    ######  @2        @3  ########$-            $-############        ################################";

	    [Test]
	    public void ParseMap()
	    {
		    ServerStuff server = new ServerStuff("s",true,50,"http://vindinium.org/","m2");
			server.CreateBoard(size, map);



			Assert.IsNotNull(server.Board);
	    }
    }
}
