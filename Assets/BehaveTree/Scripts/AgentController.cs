using UnityEngine;
using System.Collections;
using Behave.Runtime;
using Tree = Behave.Runtime.Tree;

public class AgentController : MonoBehaviour, IAgent {
	
	Tree m_Tree;	
	
	IEnumerator Start () 
	{
		m_Tree = BLAgentBehaveLib.InstantiateTree(BLAgentBehaveLib.TreeType.NewCollection1_Parallel, this);
		
		while (Application.isPlaying && m_Tree != null) 
		{
			yield return new WaitForSeconds(1.0f/m_Tree.Frequency);
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
            (BLAgentBehaveLib.IsAction(sender.ActiveID) ? "Action " : "Decorator ") + " ... " +
            (BLAgentBehaveLib.IsAction(sender.ActiveID) ? ((BLAgentBehaveLib.ActionType)sender.ActiveID).ToString() :
            ((BLAgentBehaveLib.DecoratorType)sender.ActiveID).ToString())
        );
		return BehaveResult.Success;
	}
	
	public void Reset (Tree sender)
	{
		
	}
	
	public BehaveResult TickCheckEmailAction(Tree sender)
	{
		Debug.Log("Checking email");
		return BehaveResult.Success;
	}
	
	public BehaveResult TickListenMusicAction(Tree sender)
	{
		Debug.Log("While listening music!");
		return BehaveResult.Failure;
	}		
	
	private bool isHungry = true;
	private bool isSleepy = true;
	
	public int SelectTopPriority (Tree sender, params int[] IDs)
	{	
		if (isHungry) {
			isHungry = false;
			isSleepy = true;
			return IDs[0]; //eat
		}
		else if (isSleepy) {
			isSleepy = false;
			return IDs[1]; //sleep
		}
		else {
			isHungry = true;
			return IDs[2]; //play
		}
	}
	
	private int alpha = 0;
	private int gameLoading = 0;
	
	public BehaveResult TickFadeInAction (Tree sender)
	{	
		if (gameLoading >= 100) {
			return BehaveResult.Failure;
		}
		
		alpha++;
		Debug.Log ("FadeIn ticked! Alpha:" + alpha.ToString());	
		if (alpha < 255) {
			return BehaveResult.Running;
		}
		else {
			alpha = 255;
			return BehaveResult.Success;
		}
	}
	
	public BehaveResult TickFadeOutAction (Tree sender)
	{
		alpha--;
		Debug.Log ("FadeOut ticked! Alpha:" + alpha.ToString());	
		if (alpha > 0) {
			return BehaveResult.Running;
		}
		else {
			alpha = 0;
			return BehaveResult.Success;
		}
	}
	
	public BehaveResult TickGotoGameAction (Tree sender)
	{
		gameLoading++;		
		Debug.Log ("GotoGame ticked! Game loading: " + gameLoading.ToString());		
		if (gameLoading < 100) {
			return BehaveResult.Running;
		}
		else {
			return BehaveResult.Success;
		}
	}
	
	private bool shouldDo = true;
	
	public BehaveResult TickMyActionAction (Tree sender)
	{
		Debug.Log ("MyAction ticked!");		
		return BehaveResult.Success;
	}
	
	public BehaveResult TickShouldDoMyActionDecorator (Tree sender)
	{
		shouldDo = !shouldDo;
		if (shouldDo) {
			Debug.Log ("Should Do!");
			return BehaveResult.Success;
		}
		else {		
			Debug.Log ("Shouldn't Do!");
			return BehaveResult.Failure;
		}
	}
	
	private int distWithEnemy = 200;
	private int enemyHealth = 100;
	
	public BehaveResult TickPatrolAction(Tree sender)
	{		
		if (distWithEnemy > 100) {
			distWithEnemy-=10;
			Debug.Log("Enemy is getting closers! " + distWithEnemy.ToString());
			return BehaveResult.Running;
		}
		else {
			Debug.Log("Enemy spotted!");
			return BehaveResult.Failure;
		}
	}
	
	public BehaveResult TickAttackAction(Tree sender)
	{
		enemyHealth-=5;
		Debug.Log("Attacking enemy! enemy health: " + enemyHealth.ToString ());
		if (enemyHealth < 10) {
			Debug.Log("Enemy's dead!");
			return BehaveResult.Failure;
		}
		else { 
			return BehaveResult.Running;
		}
	}
	
	public BehaveResult TickIdleAction(Tree sender)
	{		
		distWithEnemy = 200;
		enemyHealth = 100;
		Debug.Log("Idling for a while!");
		return BehaveResult.Success;
	}
}
