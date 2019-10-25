using NHapi.Base.Parser;
using NUnit.Framework;

namespace NHapi.NUnit
{
   [TestFixture]
   public partial class PipeParserTests
   {
	  private PipeParser _parser;
	  private string _message;

	  [SetUp]
	  public void SetUp()
	  {
		 _parser = new PipeParser();
		 BecauseOnce();
	  }

	  public virtual void BecauseOnce()
	  {

	  }
   }
}