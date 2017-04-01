using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirNave : MonoBehaviour {

	public Nave alvo;
	float distanciaZ;
	public float fator;
	Vector3 posAlvo;

	void Start () {
		distanciaZ = transform.position.z - alvo.transform.position.z;
	}
	
	void Update () {
		Vector3 posicao = transform.position;
		posAlvo = (Vector2) alvo.transform.position + alvo.velocidade;
		posicao = Vector2.Lerp (posicao, posAlvo, Time.deltaTime * fator);
		posicao.z = distanciaZ;
		transform.position = posicao;
		Debug.DrawLine (alvo.transform.position, posAlvo);
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireSphere (posAlvo, 0.2f);
	}
}
