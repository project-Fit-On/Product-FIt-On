import React from "react";
import styles from "./aboutus.module.css";
import Header from "../Components/Header";
import Footer from "../Components/Footer";
import Image from "next/image";
import { motion } from "framer-motion";

export default function AboutUs() {
  // Animation variants
  const fadeIn = {
    hidden: { opacity: 0, y: 20 },
    visible: { opacity: 1, y: 0, transition: { duration: 0.6 } },
  };

  return (
    <div className={styles.container}>
      <Header />

      <section className={styles.heroSection}>
        <div className={styles.heroContent}>
          <h1>About Fit-On</h1>
          <p>Your Smart Fashion Companion</p>
        </div>
      </section>

      <motion.section
        className={styles.introSection}
        initial="hidden"
        whileInView="visible"
        viewport={{ once: true }}
        variants={fadeIn}
      >
        <div className={styles.sectionContent}>
          <h2>Introduction</h2>
          <p>
            Fit-On is an innovative fashion application designed to transform
            how users interact with their wardrobes. Our platform combines
            advanced technology with user-centric design to provide a seamless
            experience for trying on clothes virtually and managing digital
            wardrobes.
          </p>
          <div className={styles.imageBubble}>
            <Image
              src="/img/aboutus-intro.svg"
              alt="Fit-On Innovation"
              width={500}
              height={300}
              className={styles.sectionImage}
            />
          </div>
        </div>
      </motion.section>

      <motion.section
        className={`${styles.offerSection} ${styles.altBackground}`}
        initial="hidden"
        whileInView="visible"
        viewport={{ once: true }}
        variants={fadeIn}
      >
        <div className={styles.sectionContent}>
          <h2>What We Offer</h2>
          <div className={styles.offerGrid}>
            <div className={styles.offerCard}>
              <div className={styles.iconContainer}>
                <i className="fas fa-cube"></i>
              </div>
              <h3>Personalized 3D Models</h3>
              <p>
                Create accurate 3D representations of yourself using LiDAR
                technology.
              </p>
            </div>

            <div className={styles.offerCard}>
              <div className={styles.iconContainer}>
                <i className="fas fa-tshirt"></i>
              </div>
              <h3>Virtual Wardrobe</h3>
              <p>Digitally store and organize your clothing collection.</p>
            </div>

            <div className={styles.offerCard}>
              <div className={styles.iconContainer}>
                <i className="fas fa-shopping-bag"></i>
              </div>
              <h3>Effortless Shopping</h3>
              <p>Preview how clothes will fit before making a purchase.</p>
            </div>

            <div className={styles.offerCard}>
              <div className={styles.iconContainer}>
                <i className="fas fa-cloud-sun"></i>
              </div>
              <h3>Weather Integration</h3>
              <p>
                Get outfit suggestions based on the current and forecasted
                weather.
              </p>
            </div>

            <div className={styles.offerCard}>
              <div className={styles.iconContainer}>
                <i className="fas fa-handshake"></i>
              </div>
              <h3>Retail Partnership</h3>
              <p>Collaborate with brands to enhance the shopping experience.</p>
            </div>
          </div>
        </div>
      </motion.section>

      <motion.section
        className={styles.visionSection}
        initial="hidden"
        whileInView="visible"
        viewport={{ once: true }}
        variants={fadeIn}
      >
        <div className={styles.sectionContent}>
          <div className={styles.visionContainer}>
            <div className={styles.visionText}>
              <h2>Our Vision</h2>
              <p>
                At Fit-On, we aim to bridge the gap between fashion and
                technology. Our mission is to provide users with a smart,
                convenient, and personalized way of managing their wardrobes and
                shopping for clothes.
              </p>
            </div>
            <div className={styles.visionImage}>
              <Image
                src="/img/vision.svg"
                alt="Fit-On Vision"
                width={400}
                height={300}
              />
            </div>
          </div>
        </div>
      </motion.section>

      <motion.section
        className={`${styles.storySection} ${styles.altBackground}`}
        initial="hidden"
        whileInView="visible"
        viewport={{ once: true }}
        variants={fadeIn}
      >
        <div className={styles.sectionContent}>
          <h2>Our Story</h2>
          <p>
            Developed by a group of enthusiastic software engineering students
            from the University of Westminster, Fit-On emerged as a solution to
            improve the online shopping experience by eliminating issues like
            incorrect sizing and inefficient wardrobe management.
          </p>
          <div className={styles.teamGrid}>
            {/* You can add team photos here if needed */}
          </div>
        </div>
      </motion.section>

      <motion.section
        className={styles.whySection}
        initial="hidden"
        whileInView="visible"
        viewport={{ once: true }}
        variants={fadeIn}
      >
        <div className={styles.sectionContent}>
          <h2>Why Choose Fit-On?</h2>
          <div className={styles.whyGrid}>
            <div className={styles.whyCard}>
              <div className={styles.whyIcon}>
                <i className="fas fa-clock"></i>
              </div>
              <p>No more wasting time on outfit selection.</p>
            </div>

            <div className={styles.whyCard}>
              <div className={styles.whyIcon}>
                <i className="fas fa-undo"></i>
              </div>
              <p>No more returns due to wrong sizes.</p>
            </div>

            <div className={styles.whyCard}>
              <div className={styles.whyIcon}>
                <i className="fas fa-shopping-cart"></i>
              </div>
              <p>Simplified shopping experience, both online and in-store.</p>
            </div>
          </div>
        </div>
      </motion.section>

      <Footer />

      {/* Font Awesome for modern icons */}
      <script
        src="https://kit.fontawesome.com/a076d05399.js"
        crossOrigin="anonymous"
      ></script>
    </div>
  );
}
