using NHapi.Model.V251.Message;
using NUnit.Framework;

namespace NHapi.NUnit
{
	public partial class PipeParserTests
	{
		public class Parses_OMG_O19 : PipeParserTests
		{
			public override void BecauseOnce()
			{
				_message =
					@"MSH|^~\&||^1.3.6.1.4.1.21367.2016.10.1.21^ISO||^1.3.6.1.4.1.21367.2016.10.1.32^ISO|20160930153834+0000||OMG^O19^OMG_O19|17882|P|2.5.1|||NE|NE|||||360X|
PID|1||T7190334^^^&1.3.6.1.4.1.21367.2016.10.1.21.5&ISO^MRN||Packton^Peter^^^L||19580817|M|
ORC|NW|889342^^1.3.6.1.4.1.21367.2016.10.1.21.15^ISO||||||||||34225PC^Allen^Anthony^M^III^MD^^^&1.3.6.1.4.1.21367.2016.10.1.21.10&ISO^L^^DN|
TQ1||||||||20161018235959+0000|
OBR||889342^^1.3.6.1.4.1.21367.2016.10.1.21.15^ISO||57133-1^^LN||||||||||||34225PC^Allen^Anthony^M^III^MD^^^&1.3.6.1.4.1.21367.2016.10.1.21.10&ISO^L^^DN|||||||||||||||^Rule out headache^|";
			}

			[Test]
			public void Parses()
			{
				var omg = _parser.Parse(_message) as OMG_O19;
				Assert.IsNotNull(omg);
			}
		}
	}
}