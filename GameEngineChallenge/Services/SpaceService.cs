using System;
using System.Collections.Generic;
using Utils.SpaceTime;

namespace GameEngineChallenge.Services
{
	public interface ISpaceService
	{
		void MoveHero( Hero hero, Vector moveVector );
		Position GetHeroPosition( Hero hero );
	}

	public class SpaceService : ISpaceService
	{
		private readonly Dictionary<Hero, Position> _heroPositions = new Dictionary<Hero, Position>();

		public void MoveHero( Hero hero, Vector moveVector ) => SetHeroPosition( hero, GetHeroPosition( hero ) + moveVector );

		public void SetHeroPosition( Hero hero, Position position ) => _heroPositions[ hero ] = position;

		public Position GetHeroPosition( Hero hero )
			=> _heroPositions.TryGetValue( hero, out Position position )
			? position
			: throw new HeroNotPlacedException();

		private class HeroNotPlacedException : Exception
		{
			public HeroNotPlacedException()
				: base( "The specified hero hasn't been placed on the map." )
			{
			}
		}
	}
}
