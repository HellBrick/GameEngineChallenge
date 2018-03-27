using System.Collections.Generic;
using Utils;

namespace GameEngineChallenge
{
	public class Hero
	{
		public Hero( TeamId team, HitPoints initialHp, params IRequisite[] initialRequisites )
		{
			Team = team;
			HP = initialHp;

			Requisites = new HashSet<IRequisite>
			(
				initialRequisites,
				RelayEqualityComparer.Create( ( IRequisite requisite ) => requisite.Id )
			);
		}

		public TeamId Team { get; }
		public ICollection<IRequisite> Requisites { get; }

		public HitPoints HP { get; set; }
	}
}
