# MnistWebApp
<p>Web application for clasifying handwritten digits.</p>
<h2>How does it work?</h2>
<h4>1. Obtaining the image</h4>
<p>
    First I needed a way for user to somehow draw the image for that I used a html5 canvas and modified the code from <a href="http://bencentra.com/code/2014/12/05/html5-canvas-touch-events.html">here.</a>
</p>
<p>Then the canvas DataUrl string is sent to the server using Ajax.</p>
<h4>2. Preprocessing the image</h4>
<p>Then the image needs to be preprocessed so it matches preprocessing done to the <a href="http://yann.lecun.com/exdb/mnist/">Mnist database of handwritten digits.</a> That is done server-side using .Net Bitmap library.</p>
<h4>3. Feeding the image to the neural network</h4>
<p>Then the image is converted to 28x28 float matrix with each element corresponding to grayscale value of one pixel and fed to the NN.</p>
<h2>How the neural network works</h2>
<p>The network has an input layer containg 784 neurons, each corresponding to grayscale value of one pixel, 2 hidden layers with 100 neurons each and 10 output neurons, each corresponding to one digit.</p>
<p>The code for the NN is taken from the book <a href="http://neuralnetworksanddeeplearning.com/chap3.html#98percent">Michael A. Nielsen, "Neural Networks and Deep Learning", Determination Press, 2015</a> and has an accuracy of over 98% on the Mnist dataset.</p>
