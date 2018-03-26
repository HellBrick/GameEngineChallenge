using System;
using System.Collections.Generic;
using GameEngineChallenge.Actions;
using Utils;
using Utils.SpaceTime;

namespace GameEngineChallenge.Abilities
{
	public class InputBasedMovementAbility : IActiveAbility
	{
		private readonly Speed _speed;

		public InputBasedMovementAbility( Speed speed ) => _speed = speed;

		public RequisiteId Id => new RequisiteId( nameof( InputBasedMovementAbility ) );
		public TickPhase Phase => CommonTickPhases.TimeAdjustments;

		public IReadOnlyCollection<IAction> GetActions( Hero abilityOwner, GameContext context )
		{
			TimeSpan timeElapsedSinceLastTick = context.TimeService.TimeElapsedSinceLastTick;
			Vector direction = context.InputService.GetDirection( abilityOwner );

			Distance moveDistance = _speed * timeElapsedSinceLastTick;
			Vector moveVector = direction.Normalize() * moveDistance.Meters;
			return new MoveAction( abilityOwner, moveVector ).AsArray();
		}
	}
}
