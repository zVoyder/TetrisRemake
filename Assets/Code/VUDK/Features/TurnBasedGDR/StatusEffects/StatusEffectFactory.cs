namespace VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects.Factory
{
    using System;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public static class StatusEffectFactory
    {
        /// <summary>
        /// Factory method of a <see cref="StatusEffectBase"/>.
        /// </summary>
        /// <param name="statusData"><see cref="StatusEffectData"/> of the status to construct.</param>
        /// <param name="unitManager"><see cref="UnitManager"/> target of the status.</param>
        /// <param name="attacksManager"><see cref="AttacksManager"/> of the status to construct.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static StatusEffectBase Construct(this StatusEffectData statusData)
        {
            if (statusData is BleedStatusData)
                return new BleedStatus(statusData);

            if (statusData is DamageDownStatusData)
                return new DamageDownStatus(statusData);

            if (statusData is DamageUpStatusData)
                return new DamageUpStatus(statusData);

            if (statusData is DamageReductionStatusData)
                return new DamageReductionStatus(statusData);

            if (statusData is FearStatusData)
                return new FearStatus(statusData);

            if (statusData is StunStatusData)
                return new StunStatus(statusData);

            throw new Exception($"Type of {nameof(statusData)} could not be instantiated because was not found.");
        }
    }
}