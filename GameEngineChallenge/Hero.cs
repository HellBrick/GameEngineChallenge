using System.Collections.Generic;
using Utils;

namespace GameEngineChallenge
{
	public class Hero
	{
		public Hero( params IRequisite[] initialRequisites )
		{
			Requisites = new HashSet<IRequisite>
			(
				initialRequisites,
				RelayEqualityComparer.Create( ( IRequisite requisite ) => requisite.Id )
			);
		}

		public ICollection<IRequisite> Requisites { get; }
	}
}
