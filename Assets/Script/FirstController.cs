using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {
	private IUserAction action;
    public DiskFactory diskFactory;
    public UserGUI userGui;
    public ScoreRecorder scoreRecorder;
	public RoundController roundControl;
	public IActionManager actionManager,actionManager2;

    private Queue<GameObject> disk_queue = new Queue<GameObject>();          //游戏场景中的飞碟
    private List<GameObject> disk_alive = new List<GameObject>();          //没有被打中的飞碟
    private bool playing = false;
	private int throwNum;
	private float speed;
	private float time = 0;
	private Vector3 direction;
	public Color[] setColor = {Color.white,Color.black,Color.yellow,Color.blue,Color.green,Color.red,};


    void Start () {
        SSDirector director = SSDirector.GetInstance();
		director.CurrentScenceController = this;
		diskFactory = Singleton<DiskFactory>.Instance;
        scoreRecorder = Singleton<ScoreRecorder>.Instance;
        userGui = gameObject.AddComponent<UserGUI>() as UserGUI;
		roundControl = Singleton<RoundController>.Instance;
		actionManager = gameObject.AddComponent<PhysisActionManager>() as IActionManager;
		actionManager2 = gameObject.AddComponent<CCActionManager>() as IActionManager;
		speed = 2f;
    }

	void Update () {
        if(playing) {
			if (Input.GetButtonDown("Fire1")) {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			        RaycastHit hit;
			            //射线打中物体
			            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "disk") {
			                disk_alive.Remove(hit.collider.gameObject);
							hit.collider.gameObject.SetActive(false);
							hit.collider.gameObject.transform.position = new Vector3(0,-9,0);
			                //记分员记录分数
			                scoreRecorder.Record();
							diskFactory.resetDisk(hit.collider.gameObject);
						}
	            }
			for (int i = 0; i < disk_alive.Count; i++)
		        {
		            GameObject temp = disk_alive[i];
		            if (temp.transform.position.y < -6 && temp.gameObject.activeSelf == true)
		            {
		                diskFactory.resetDisk(disk_alive[i]);
		                disk_alive.Remove(disk_alive[i]);
		                userGui.MissUFO();
		            }
		        }
            //游戏结束
            if (roundControl.getRound() == 4) {
				gameOver();
                userGui._gameOver();
				playing = false;
            }
            //发送飞碟
			time += Time.deltaTime;
			if (time > 3) {
				time = 0;
				LoadResources();
			}
        }
    }

    public void LoadResources()
    {
		throwNum = roundControl.getUFONum();
        for (int i = 0;i < throwNum;i++) {
			disk_queue.Enqueue(diskFactory.GetDisk());
			throwDisk();
		}
		roundControl.setRound();
    }

    private void throwDisk() {
        if (disk_queue.Count != 0) {
            GameObject disk = disk_queue.Dequeue();
            disk_alive.Add(disk);
            disk.SetActive(true);
			int chooseColor = Random.Range(0, 5);
            disk.GetComponent<Renderer>().sharedMaterial.color = setColor[chooseColor];
            disk.transform.position = new Vector3(Random.Range(-1f,6f), Random.Range(1f, 4f), 0);
            direction.x = Random.Range(-1,1);
			if (direction.x < 0) {
				direction.x = -1;
				disk.transform.position = new Vector3(Random.Range(0f,6f), Random.Range(1f, 4f), 0);
			} else {
				direction.x = 1;
				disk.transform.position = new Vector3(Random.Range(-6f,-1f), Random.Range(1f, 4f), 0);
			}
			float power = Random.Range(5,10);
            actionManager.throwUFO(disk,direction,power);
        }

    }
	public int ShowScore()	{
	   return scoreRecorder.score;
   }
   public int ShowRound() {
	   return roundControl.getRound();
   }
   public void gameStart() {
	   playing = true;
	   roundControl.reset();
	   scoreRecorder.Reset();
   }
   public void switchActionType(){
	   IActionManager temp = actionManager;
	   actionManager = actionManager2;
	   actionManager2 = temp;
   }
   public void gameOver() {
	   playing = false;
   }
}
