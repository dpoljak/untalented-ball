﻿using UnityEngine;

public class SimpleProjectile : Projectile, ITakeDamage
{
    public int Damage;
    public GameObject DestroyedEffect;
    public int PointsToGiveToplayer;
    public float TimeToLive;
    public AudioClip DestroySound;

    public void Update()
    {
        if ((TimeToLive -= Time.deltaTime) <= 0)
        {
            DestroyProjectile();
            return;
        }

        transform.Translate(Direction * ((Mathf.Abs(InitialVelocity.x) + Speed) *Time.deltaTime), Space.World);
    }

    public void TakeDamage(int damage, GameObject instigator)
    {
        if (PointsToGiveToplayer != 0)
        {
            var projectile = instigator.GetComponent<Projectile>();
            if (projectile != null && projectile.Owner.GetComponent<Player>() != null)
            {
                GameManager.Instance.AddPoints(PointsToGiveToplayer);
                FloatingText.Show(string.Format("+{0}!", PointsToGiveToplayer), "PointStarText",
                    new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));
            }
        }

        DestroyProjectile();
    }

    protected override void OnCollideOther(Collider2D other)
    {
        DestroyProjectile();
    }

    protected override void OnCollideTakeDamage(Collider2D other, ITakeDamage takeDamage)
    {
        takeDamage.TakeDamage(Damage, gameObject);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        if (DestroyedEffect != null)
            Instantiate(DestroyedEffect, transform.position, transform.rotation);

        if (DestroySound != null)
            AudioSource.PlayClipAtPoint(DestroySound, transform.position);

        Destroy(gameObject);
    }
}
