using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : Entity
{
    public GameObject Explosion;

    public CinemachineVirtualCamera VCam;

    public override void Die()
    {
        Debug.Log("You lost!");
        VCam.Follow = transform;
        FindObjectOfType<Player>().IsDead = true;
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        MakeExplosion(new Vector3(-.5f, 1), 2, 60);
        yield return new WaitForSeconds(.5f);
        MakeExplosion(new Vector3(.5f, 1.5f), 2, -60);
        yield return new WaitForSeconds(.5f);
        MakeExplosion(new Vector3(-.2f, 2f), 2, 45);
        yield return new WaitForSeconds(.5f);
        GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");

    }

    private void MakeExplosion(Vector3 offset, float scale, float rotation)
    {
        GameObject explosion = Instantiate(Explosion, transform.position + offset, Quaternion.Euler(Vector3.forward * rotation));
        explosion.transform.localScale = Vector3.one * scale;
    }

    public override void Move()
    {
        //I don't move
    }


}
