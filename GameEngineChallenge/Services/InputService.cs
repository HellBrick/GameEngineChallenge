using System.Collections.Generic;
using Utils.SpaceTime;

namespace GameEngineChallenge.Services
{
	public interface IInputService
	{
		Vector GetDirection( Hero hero );
	}

	public class InputService : IInputService
	{
		private readonly Dictionary<Hero, Vector> _heroDirections = new Dictionary<Hero, Vector>();

		public Vector GetDirection( Hero hero )
			=> _heroDirections.TryGetValue( hero, out Vector direction )
			? direction
			: default;

		public void SetDirection( Hero hero, Vector direction ) => _heroDirections[ hero ] = direction;
	}
}
