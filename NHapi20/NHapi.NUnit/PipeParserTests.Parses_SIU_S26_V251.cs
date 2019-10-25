using NHapi.Model.V251.Message;
using NUnit.Framework;

namespace NHapi.NUnit
{
	public partial class PipeParserTests
	{
		public class Parses_SIU_S26_V251 : PipeParserTests
		{
			public override void BecauseOnce()
			{
				_message = @"MSH|^~\&||^1.3.63.998.999.3^ISO||^1.3.63.5444.345.2.1^ISO|20161010172813+0000||SIU^S26^SIU_S26|25882|P|2.5.1|||NE|NE|||||360X|
SCH||18467^^1.3.6.1.4.1.21367.2016.10.1.32.14^ISO||||57133-1^^LN||||||||||^Name^Registrar||||^Name^Enterer||||||889342^^1.3.6.1.4.1.21367.2016.10.1.21.15^ISO|
TQ1|||||||20161009140000+0000|20161009143000+0000|
PID|1||T7190334^^^&1.3.6.1.4.1.21367.2016.10.1.21.5&ISO^MRN~L53HG67^^^&1.3.6.1.4.1.21367.2016.10.1.32.11&ISO^MRN||Packton^Peter^^^L||19580817|M|
RGS|1|D|";
			}

			[Test]
			public void Parses()
			{
				var siu = _parser.Parse(_message) as SIU_S26;
				Assert.IsNotNull(siu);
			}
		}
	}
}