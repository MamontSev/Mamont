
using Mamont.Gameplay.Level.Colliers;

using UnityEngine;
using UnityEngine.AI;

namespace Mamont.Gameplay.Pers.Mob
{
	public class MobIdleWalker
	{
		public MobIdleWalker
		(
			MobBasic _mobBasic 
		)
		{
			this._navMeshAgent = _mobBasic.NavMeshAgent;
			this._inContainer = _mobBasic.InContainer;
		}

		private NavMeshAgent _navMeshAgent;
		private Transform _inContainer;


		private IdleState _idleState = IdleState.stay;
		private enum IdleState
		{
			stay,													 
			findWalkPoint,
			walkToPoint
		}

		public void Init( Vector3 _startIdleWalkPos)
		{
			this._startIdleWalkPos = _startIdleWalkPos;
			_timeIdleWaitToGo = 0.0f;
			_idleState = IdleState.findWalkPoint;
		}

		private Vector3 _startIdleWalkPos;
		private float _timeIdleWaitToGo = 0.0f;
		public void Update()
		{
			if( _idleState == IdleState.findWalkPoint )
			{
				float bigRadius = 55.0f;
				float smallRadius = 3.5f;
				float delta = bigRadius - smallRadius;
				float length = smallRadius + delta * Random.value;
				Vector3 randomPoint = Random.insideUnitSphere.normalized * length;

				randomPoint += _startIdleWalkPos;
				randomPoint.y = _startIdleWalkPos.y;

				NavMeshHit hit;
				bool comp = NavMesh.SamplePosition(randomPoint , out hit , 1.0f , _navMeshAgent.areaMask);
				if( comp == false )
				{
					return;
				}

				Vector3 v1 = new Vector3(_inContainer.transform.position.x , _inContainer.transform.position.y + 1.0f , _inContainer.transform.position.z);
				Vector3 v2 = new Vector3(hit.position.x , hit.position.y + 1.0f , hit.position.z);
				float dist = Vector3.Distance(v1 , v2);
				Debug.DrawRay(v1 , v2 - v1 , Color.green , 0.5f , false);
				RaycastHit[] hits = Physics.RaycastAll(v1 , v2 - v1 , dist);

				bool findObstacle = false;
				for( int i = 0; i < hits.Length; i++ )
				{
					RaycastHit raycastHit = hits[i];
					if( raycastHit.transform == null )
					{
						continue;
					}
					bool b = raycastHit.transform.TryGetComponent(out IMobObstacleCollider obstacle);
					if( b == true )
					{
						findObstacle = true;
						break;
					}
				}
				if( findObstacle == true )
				{
					return;
				}

				Vector3 point = hit.position;
				_navMeshAgent.SetDestination(point);
				_navMeshAgent.stoppingDistance = 0.2f;
				_idleState = IdleState.walkToPoint;
			}
			else if( _idleState == IdleState.walkToPoint )
			{
				if( _navMeshAgent.pathPending == false )
				{
					if( _navMeshAgent.remainingDistance < ( _navMeshAgent.stoppingDistance + 0.2 ) )
					{
						_idleState = IdleState.stay;
						_timeIdleWaitToGo = Time.time + 0.1f + Random.value * 0.0f;
					}
				}
			}
			else if( _idleState == IdleState.stay )
			{
				if( Time.time > _timeIdleWaitToGo )
				{
					_idleState = IdleState.findWalkPoint;
				}
			}
		}
	}
}
