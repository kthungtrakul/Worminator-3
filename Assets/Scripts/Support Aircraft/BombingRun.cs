using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombingRun : MonoBehaviour
{
    /*
     * The bombing run is a type of fire support geared towards taking out
     * fortified targets. These can be groups of turrets, artillery batteries, or forts.
     * Bombers drop a precision cluster bomb with a large blast radius. The submunitions inside
     * carry the weight of an artillery shell, so use it to take out tough targets.
     *
     * By contrast, the missile strike uses precision missiles launched in waves to take out a single target.
     * The player can choose between a regular missile strike or a precision bomb (similar to HAWX's stand-off missiles) to destroy a target.
     *
     * The player initiates a bombing run provided there are no SAM sites hindering aircraft from accessing the site.
     * Use a different fire support method or a player weapon to destroy the SAM site. Anti-artillery can try to destroy the missile, but it may
     * not always destroy it in time.
     *
     * Step 1: Player uses a marker to specify the target location.
     * Step 2: The player must retreat to a safe location away from the marker point. From that point, specify the direction of bombing using the radar map.
     * This is important if the location to be hit is between two SAM sites. The SAMs used are strong enough to shoot down a bomber with one missile.
     * Step 3: All support aircraft get in a triangular formation and fly towards the target in the indicated direction.
     *   >          >
     *      >           >
     *   >          >
     * The number of waves depends on the number of aircraft in the scene. A non-multiple of 3 means the smaller aircraft conduct the bombing run
     * while the larger planes stay behind and provide anti-artillery coverage.
     * Step 4: The planes break formation and resume their normal flight pattern.
     */
    public GameObject Target { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
