using System;
using FluentAssertions;
using GameEngineChallenge.Abilities;
using Xunit;

namespace GameEngineChallenge.Test
{
	public class CooldownAbilityTest
	{
		private static readonly TimeSpan _cooldown = TimeSpan.FromSeconds( 1 );

		[Fact]
		public void CooldownIsEnabledAfterFirstInvocation()
		{
			CooldownAbility cooldownAbility
				= RequisiteHelper
				.CreateActiveAbility( phase: default, ( h, c ) => { } )
				.WithCooldown( _cooldown );

			cooldownAbility.GetActions( default, default ).Should().HaveCount( 1 );
			cooldownAbility.TimeLeft.Should().Be( _cooldown );
		}

		[Fact]
		public void CooldownIsNotEnabledIfAbilityDoesNotEmitAnyActions()
		{
			CooldownAbility cooldownAbility
				= RequisiteHelper
				.CreateActiveAbility( phase: default, ( h, c ) => Array.Empty<IAction>() )
				.WithCooldown( _cooldown );

			cooldownAbility.GetActions( default, default ).Should().HaveCount( 0 );
			cooldownAbility.TimeLeft.Should().Be( TimeSpan.Zero );
		}

		[Fact]
		public void NoActionsAreEmittedIfCooldownIsEnabled()
		{
			CooldownAbility cooldownAbility
				= RequisiteHelper
				.CreateActiveAbility( phase: default, ( h, c ) => { } )
				.WithCooldown( _cooldown );

			cooldownAbility.TimeLeft = TimeSpan.FromSeconds( 1 );
			cooldownAbility.GetActions( default, default ).Should().HaveCount( 0 );
		}
	}
}
