using NHapi.Model.V251.Message;
using NUnit.Framework;

namespace NHapi.NUnit
{
   public partial class PipeParserTests
   {
	  public class Parses_OSU_O51 : PipeParserTests
	  {
		 public override void BecauseOnce()
		 {
			_message =
				@"MSH|^~\&||^1.3.6.1.4.1.21367.2016.10.1.21^ISO||^1.3.6.1.4.1.21367.2016.10.1.32^ISO|20161007092857+0000||OSU^O51^OSU_O51|23882|P|2.5.1|||NE|NE|||||360X|
PID|1||T7190334^^^&1.3.6.1.4.1.21367.2016.10.1.21.5&ISO^MRN||Packton^Peter^^^L||19580817|M|
ORC|CA|889342^^1.3.6.1.4.1.21367.2016.10.1.21.15^ISO||||||||||34225PC^Allen^Anthony^M^III^MD^^^&1.3.6.1.4.1.21367.2016.10.1.21.10&ISO^L^^DN||||^Headache disappeared|";
		 }

		 [Test]
		 public void Parses()
		 {
			var osu = _parser.Parse(_message) as OSU_O51;
			Assert.IsNotNull(osu);
		 }
	  }
   }
}