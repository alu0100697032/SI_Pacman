using UnityEngine;
using System.Collections;
using Behave.Runtime;
using Tree = Behave.Runtime.Tree;
using System;

public interface IAgent
{
    BehaveResult Tick(Tree sender, bool init);
    void Reset(Tree sender);
    int SelectTopPriority(Tree sender, params int[] IDs);
}

public class AgentController : MonoBehaviour, IAgent
{
    Tree m_Tree;

    IEnumerator Start()
    {
        m_Tree = BLAgentBehaveLib.InstantiateTree(
        BLAgentBehaveLib.TreeType.NewCollection1_NewTree1, (Behave.Runtime.IAgent)this);
        while (Application.isPlaying && m_Tree != null)
        {
            yield return new
            WaitForSeconds(1.0f / m_Tree.Frequency);
            AIUpdate();
        }
    }

    void AIUpdate()
    {
        m_Tree.Tick();
    }

    public BehaveResult Tick(Tree sender, bool init)
    {
        Debug.Log("Ticked Received by unhandled " +
        (BLAgentBehaveLib.IsAction(sender.ActiveID) ? "Action " :
        "Decorator ") +
        " ... " + (BLAgentBehaveLib.IsAction(sender.ActiveID) ?
        ((BLAgentBehaveLib.ActionType)sender.ActiveID).ToString() :
        ((BLAgentBehaveLib.DecoratorType)sender.ActiveID).ToString()));
        return BehaveResult.Success;
    }

    public void Reset(Tree sender)
    {
    }

    public int SelectTopPriority(Tree sender, params int[] IDs)
    {
        return 0;
    }
}
