using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Base.validation.impl;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
   public class Test
	{
		private PipeParser _parser;

		[SetUp]
	   public void SetUp()
	   {
			_parser = new PipeParser();
		}


	   public void TestParserEncoding()
	   {

	   }
   }

   public class OSU_O51 : AbstractMessage
   {
	  public MSH MSH { get; set; }
	  public PID PID { get; set; }
	  public ORC ORC { get; set; }

	  public OSU_O51(OMG_O19 message) : base(new CustomModelClassFactory())
	  {
		 add(typeof(MSH), true, false);
		 add(typeof(PID), true, false);
		 add(typeof(ORC), true, false);
		 AddStructure("MSH");
		 AddStructure("PID");
		 AddStructure("ORC");

		 ValidationContext = new DefaultValidation();

		 MSH = new MSH(this, new DefaultModelClassFactory());

		 MSH.SendingFacility.UniversalID.Value = message.MSH.ReceivingFacility.UniversalID.Value;
		 MSH.SendingFacility.NamespaceID.Value = message.MSH.ReceivingFacility.NamespaceID.Value;
		 MSH.SendingFacility.UniversalIDType.Value = message.MSH.ReceivingFacility.UniversalIDType.Value;
		 MSH.ReceivingFacility.UniversalID.Value = message.MSH.SendingFacility.UniversalID.Value;
		 MSH.ReceivingFacility.NamespaceID.Value = message.MSH.SendingFacility.NamespaceID.Value;
		 MSH.ReceivingFacility.UniversalIDType.Value = message.MSH.SendingFacility.UniversalIDType.Value;
		 MSH.FieldSeparator.Value = message.MSH.FieldSeparator.Value;
		 MSH.EncodingCharacters.Value = message.MSH.EncodingCharacters.Value;
		 MSH.MessageType.MessageStructure.Value = "OSU_O51";
		 MSH.MessageType.MessageCode.Value = "OSU";
		 MSH.MessageType.TriggerEvent.Value = "O51";
		 MSH.MessageControlID.Value = Guid.NewGuid().ToString("N");
		 MSH.ProcessingID.ProcessingID.Value = "P";
		 MSH.VersionID.VersionID.Value = "2.5.1";
		 var profileId = MSH.AddMessageProfileIdentifier();
		 profileId.NamespaceID.Value = "360x";
		 var result = MSH.GetMessageProfileIdentifier(0);
		 MSH.AcceptAcknowledgmentType.Value = "NE";
		 MSH.ApplicationAcknowledgmentType.Value = "NE";

		 PID = message.PATIENT.PID;
		 //PID.AdministrativeSex.Value = message.PATIENT.PID.AdministrativeSex.Value;
		 //PID.BirthOrder.Value = message.PATIENT.PID.BirthOrder.Value;
		 //PID.BirthPlace.Value = message.PATIENT.PID.BirthPlace.Value;

		 ORC = new ORC(this, new DefaultModelClassFactory());
		 ORC.PlacerOrderNumber.NamespaceID.Value = Guid.NewGuid().ToString();
		 ORC.PlacerOrderNumber.UniversalIDType.Value = "ISO";
	  }

	  public void Respond(string organizationObjectId, AcceptanceType acceptType)
	  {
		 ORC.PlacerOrderNumber.UniversalID.Value = organizationObjectId;
		 ORC.OrderControlCodeReason.Text.Value = acceptType.ToString();
		 switch (acceptType)
		 {
			case AcceptanceType.Accepted:
			   ORC.OrderControl.Value = OrderControlCode.OK.ToString();
			   ORC.OrderStatus.Value = OrderStatus.IP.ToString();
			   break;
			case AcceptanceType.Declined:
			   ORC.OrderControl.Value = OrderControlCode.UA.ToString();
			   ORC.OrderStatus.Value = OrderStatus.CA.ToString();
			   break;
		 }

		 var theDateTime = DateTime.UtcNow;
		 ORC.DateTimeOfTransaction.Time.SetLongDate(theDateTime);
		 MSH.DateTimeOfMessage.Time.SetLongDate(theDateTime);
	  }

	  public override string ToString()
	  {
		 var fs = MSH.FieldSeparator.Value;
		 var ss = MSH.EncodingCharacters.Value[0];
		 var sb = new StringBuilder();
		 var mshParts = new[]
		 {
				"MSH",
				MSH.EncodingCharacters.Value,
				GetString(MSH.SendingApplication, ss),
				GetString(MSH.SendingFacility, ss),
				GetString(MSH.ReceivingApplication, ss),
				GetString(MSH.ReceivingFacility, ss),
				GetLongTimeString(MSH.DateTimeOfMessage),
				MSH.Security.Value,
				GetString(MSH.MessageType, ss),
				MSH.MessageControlID.Value,
				GetString(MSH.ProcessingID, ss),
				MSH.VersionID.VersionID.Value,
				MSH.SequenceNumber.Value,
				MSH.ContinuationPointer.Value,
				"NE",
				"NE",
				MSH.CountryCode.Value,
				MSH.GetCharacterSet(0).Value,
				"",
				"",
				"360x",
				""
			};

		 var pidParts = new[]
		 {
				"PID",
				"1",
				GetString(PID.PatientID, ss),
				GetString(PID.GetPatientIdentifierList(), ss),
				GetString(PID.GetAlternatePatientIDPID(), ss),
				GetString(PID.GetPatientName(), ss),
				GetShortTimeString(PID.DateTimeOfBirth),
				PID.AdministrativeSex.Value,
				""
			};


		 return $"{string.Join(fs, mshParts)}{Environment.NewLine}{string.Join(fs, pidParts)}{Environment.NewLine}";
		 //sb.Append($"MSH{fs}{MSH.EncodingCharacters.Value}{fs}{GetString(MSH.SendingApplication, ss)}{fs}{GetString(MSH.SendingFacility, ss)}");
	  }

	  public string GetShortTimeString(TS ts)
	  {
		 return $"{ts.Time.Year}{ts.Time.Month:d2}{ts.Time.Day:d2}";
	  }

	  public string GetLongTimeString(TS ts)
	  {
		 return
			 $"{ts.Time.Year}{ts.Time.Month:d2}{ts.Time.Day:d2}{ts.Time.Hour:d2}{ts.Time.Minute:d2}{ts.Time.Second:d2}+0000";
	  }

	  public string GetString(HD hdType, char ss)
	  {
		 var sb = new StringBuilder();
		 if (!string.IsNullOrWhiteSpace(hdType.NamespaceID.Value))
			sb.Append($"{hdType.NamespaceID.Value}");
		 if (!string.IsNullOrWhiteSpace(hdType.UniversalID.Value))
			sb.Append($"{ss}{hdType.UniversalID.Value}");
		 if (!string.IsNullOrWhiteSpace(hdType.UniversalIDType.Value))
			sb.Append($"{ss}{hdType.UniversalIDType.Value}");
		 return sb.ToString();
	  }

	  public string GetString(MSG message, char ss)
	  {
		 var sb = new StringBuilder();
		 if (!string.IsNullOrWhiteSpace(message.MessageCode.Value))
			sb.Append($"{message.MessageCode.Value}");
		 if (!string.IsNullOrWhiteSpace(message.TriggerEvent.Value))
			sb.Append($"{ss}{message.TriggerEvent.Value}");
		 if (!string.IsNullOrWhiteSpace(message.MessageStructure.Value))
			sb.Append($"{ss}{message.MessageStructure.Value}");
		 return sb.ToString();
	  }

	  public string GetString(PT pt, char ss)
	  {
		 var sb = new StringBuilder();
		 if (!string.IsNullOrWhiteSpace(pt.ProcessingID.Value))
			sb.Append($"{pt.ProcessingID.Value}");
		 if (!string.IsNullOrWhiteSpace(pt.ProcessingMode.Value))
			sb.Append($"{ss}{pt.ProcessingMode.Value}");
		 return sb.ToString();
	  }


	  public string GetString(CX[] cxs, char ss)
	  {
		 var result = cxs.Select(cx => GetString(cx, ss)).ToList();
		 return string.Join("~", result);
	  }

	  public string GetString(CX cx, char ss)
	  {
		 var sb = new StringBuilder();
		 if (!string.IsNullOrWhiteSpace(cx.IDNumber.Value))
		 {
			sb.Append(cx.IDNumber.Value);
			sb.Append($"{ss}{ss}"); //check digit, check digit scheme
		 }

		 var assigningAuthorityString = GetString(cx.AssigningAuthority, '&');
		 if (!string.IsNullOrWhiteSpace(assigningAuthorityString))
			sb.Append($"{ss}{assigningAuthorityString}");
		 if (!string.IsNullOrWhiteSpace(cx.IdentifierTypeCode.Value))
			sb.Append($"{ss}{cx.IdentifierTypeCode.Value}");
		 return sb.ToString();
	  }

	  public string GetString(XPN[] xpns, char ss)
	  {
		 var result = new List<string>();
		 foreach (var xpn in xpns)
		 {
			var sb = new StringBuilder();
			//if (!string.IsNullOrWhiteSpace(xpn.FamilyName.Surname.Value))
			sb.Append(xpn.FamilyName.Surname.Value);
			//if (!string.IsNullOrWhiteSpace(xpn.GivenName.Value))
			sb.Append($"{ss}{xpn.GivenName.Value}");
			//if (!string.IsNullOrWhiteSpace(xpn.SecondAndFurtherGivenNamesOrInitialsThereof.Value))
			sb.Append($"{ss}{xpn.SecondAndFurtherGivenNamesOrInitialsThereof.Value}");
			//if (!string.IsNullOrWhiteSpace(xpn.SuffixEgJRorIII.Value))
			sb.Append($"{ss}{xpn.SuffixEgJRorIII.Value}");
			//if (!string.IsNullOrWhiteSpace(xpn.NameTypeCode.Value))
			sb.Append($"{ss}{xpn.PrefixEgDR.Value}");
			sb.Append($"{ss}{xpn.NameTypeCode.Value}");
			result.Add(sb.ToString());
		 }

		 return string.Join("&", result);
	  }
   }

}
