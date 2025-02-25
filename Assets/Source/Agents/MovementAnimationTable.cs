namespace Agent
{



    public partial class AgentMovementAnimationTable
    {


        public int MovementStateCount = System.Enum.GetNames(typeof(Enums.AgentMovementState)).Length;


        public int AgentAnimationTypeCount = System.Enum.GetNames(typeof(Enums.AgentAnimationType)).Length;
        public int AnimationSetCount = System.Enum.GetNames(typeof(Enums.ItemAnimationSet)).Length;



        public AgentAnimation[][][] AnimationTable;


        public void InitStage1()
        {
            AnimationTable = new AgentAnimation[MovementStateCount][][];

            for(int movementStateIndex = 0; movementStateIndex < MovementStateCount; movementStateIndex++)
            {
                AnimationTable[movementStateIndex] = new AgentAnimation[AgentAnimationTypeCount][];

                for(int animationTypeIndex = 0; animationTypeIndex < AgentAnimationTypeCount; animationTypeIndex++)
                {
                    AnimationTable[movementStateIndex][animationTypeIndex] = new AgentAnimation[AnimationSetCount];
                }
            }
        }



        public void InitStage2()
        {
           InitSpaceMarineAnimations();
            InitInsectAnimations();
        }


        public AgentAnimation GetAnimation(Enums.AgentMovementState movementState, Enums.AgentAnimationType animationType,
            Enums.ItemAnimationSet animationSet)
        {
            return AnimationTable[(int)movementState][(int)animationType][(int)animationSet];
        }


        public void SetAnimation(Enums.AgentMovementState movementState, Enums.AgentAnimationType animationType,
            Enums.ItemAnimationSet  animationSet, AgentAnimation animation)
        {
            AnimationTable[(int)movementState][(int)animationType][(int)animationSet] = animation;
        }

    }
}