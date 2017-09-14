var scene = (function() {
    "use strict";
    var scene = new THREE.Scene();
    var renderer = new THREE.WebGLRenderer({alpha: true});

    var ambientLight = new THREE.AmbientLight( 0xffffff, 0.2 );
    var light = new THREE.DirectionalLight( 0xffffff, 0.8 );

    var camera, earthMesh;

    function initScene() {
        renderer.setSize(window.innerWidth, window.innerHeight);
        document.getElementById('container').appendChild(renderer.domElement);

        camera = new THREE.PerspectiveCamera(
            35,
            window.innerWidth / window.innerHeight,
            1,
            1000
        );
        camera.position.set(0, 0, 5);
        scene.add(camera);

        light.position.set( 1, 1, 1 );

        scene.add( ambientLight );
        scene.add( light );

        var loader = new THREE.TextureLoader();

        var geometry = new THREE.SphereGeometry(1, 64, 64);
        var material = new THREE.MeshLambertMaterial( { color: 0xffffff } );
        material.map = loader.load('earth.jpg', function ( texture ) {
                var material = new THREE.MeshBasicMaterial( {
                    map: texture
                } );
            },
            // Function called when download progresses
            function ( xhr ) {
                console.log( (xhr.loaded / xhr.total * 100) + '% loaded' );
            },
            // Function called when download errors
            function ( xhr ) {
                console.log( 'An error happened' );
            }
        );

        earthMesh = new THREE.Mesh( geometry, material );
        scene.add( earthMesh );

        render();
    }

    function render() {
        earthMesh.rotation.y += 0.015;

        renderer.render(scene, camera);
        requestAnimationFrame(render);
    }

    return {
        initScene: initScene
    }
})();