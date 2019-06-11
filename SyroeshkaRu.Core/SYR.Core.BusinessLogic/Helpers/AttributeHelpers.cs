using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using SYR.Core.BusinessLogic.ViewModel;

namespace SYR.Core.BusinessLogic.Helpers
{
	//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	//public class SequrityAttribute : Attribute
	//{
	//	private readonly ICollection<SequrityProfilesViewModel> _db = new List<SequrityProfilesViewModel>();
	//	public ICollection<string> SequrityProfile
	//	{
	//		get
	//		{
	//			return _db.Select(i => i.Name).Where(i => i.Contains(SequrityProfile));
	//		}
	//	}

	//	public SequrityAttribute(string sequrityProfile)
	//	{
	//		sequrityProfile = this.SequrityProfile.FirstOrDefault();
	//	}

	//	public SequrityAttribute()
	//	{

	//	}
	//}
	//public class SequrityProfile
	//{
	//	private static readonly ICollection<SequrityProfilesViewModel> _db = new List<SequrityProfilesViewModel>();
	//	public static string Profile(string profile)
	//	{
	//		return _db.FirstOrDefault(i => i.Name == profile)?.SequrityRoles.Select(i => i.Roles.Name).ToString();
	//	}
	//}
}
