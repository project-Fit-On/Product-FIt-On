"use client";
import React, { useEffect, useRef, useState } from "react";
import styles from "./HowItWorks.module.css";

const HowItWorks = () => {
  const containerRef = useRef(null);
  const sectionRefs = useRef([]);
  const [activeSection, setActiveSection] = useState(0);
  const [isScrolling, setIsScrolling] = useState(false);
  const [touchStart, setTouchStart] = useState(null);
  const [touchEnd, setTouchEnd] = useState(null);

  useEffect(() => {
    const handleScroll = () => {
      if (!containerRef.current || isScrolling) return;

      const scrollPosition = containerRef.current.scrollLeft;
      const containerWidth = containerRef.current.offsetWidth;

      // Calculate which section is in view
      const sectionIndex = Math.round(scrollPosition / containerWidth);

      if (
        sectionIndex !== activeSection &&
        sectionIndex >= 0 &&
        sectionIndex <= 2
      ) {
        setActiveSection(sectionIndex);
      }

      // Add visible class to sections in view
      sectionRefs.current.forEach((section, index) => {
        if (!section) return;

        const rect = section.getBoundingClientRect();
        const isInView = rect.left < window.innerWidth && rect.right > 0;

        if (isInView) {
          section.classList.add(styles.visible);
        }
      });
    };

    const container = containerRef.current;
    if (container) {
      container.addEventListener("scroll", handleScroll, { passive: true });
      // Trigger initial check
      handleScroll();
    }

    // Handle vertical scrolling to navigate sections
    const handleWheelScroll = (e) => {
      if (!containerRef.current || isScrolling) return;

      e.preventDefault();
      setIsScrolling(true);

      const direction = e.deltaY > 0 ? 1 : -1;
      const newSection = Math.min(Math.max(activeSection + direction, 0), 2);

      if (newSection !== activeSection) {
        scrollToSection(newSection);
      }

      // Prevent too rapid scrolling
      setTimeout(() => setIsScrolling(false), 800);
    };

    const mainContainer = document.querySelector(
      `.${styles.howItWorksContainer}`
    );
    if (mainContainer) {
      mainContainer.addEventListener("wheel", handleWheelScroll, {
        passive: false,
      });
    }

    return () => {
      if (container) {
        container.removeEventListener("scroll", handleScroll);
      }
      if (mainContainer) {
        mainContainer.removeEventListener("wheel", handleWheelScroll);
      }
    };
  }, [activeSection, isScrolling]);

  // Improved touch event handlers for better mobile swipe support
  const handleTouchStart = (e) => {
    setTouchStart(e.touches[0].clientX);
    setTouchEnd(null); // Reset touchEnd on new touch
  };

  const handleTouchMove = (e) => {
    setTouchEnd(e.touches[0].clientX);
  };

  const handleTouchEnd = () => {
    if (!touchStart || !touchEnd || isScrolling) return;

    const diff = touchStart - touchEnd;
    const threshold = 50; // Minimum swipe distance

    // Only process if significant swipe detected
    if (Math.abs(diff) > threshold) {
      setIsScrolling(true);

      // Swipe left (next section)
      if (diff > 0) {
        const newSection = Math.min(activeSection + 1, 2);
        if (newSection !== activeSection) {
          scrollToSection(newSection);
        }
      }
      // Swipe right (previous section)
      else {
        const newSection = Math.max(activeSection - 1, 0);
        if (newSection !== activeSection) {
          scrollToSection(newSection);
        }
      }

      setTimeout(() => setIsScrolling(false), 800);
    }

    setTouchStart(null);
    setTouchEnd(null);
  };

  const scrollToSection = (index) => {
    if (!containerRef.current) return;

    const sectionWidth = containerRef.current.offsetWidth;
    containerRef.current.scrollTo({
      left: index * sectionWidth,
      behavior: "smooth",
    });
    setActiveSection(index);
  };

  // Track keyboard navigation
  useEffect(() => {
    const handleKeyDown = (e) => {
      if (isScrolling) return;

      if (e.key === "ArrowRight" || e.key === "ArrowDown") {
        e.preventDefault();
        const newSection = Math.min(activeSection + 1, 2);
        if (newSection !== activeSection) {
          setIsScrolling(true);
          scrollToSection(newSection);
          setTimeout(() => setIsScrolling(false), 800);
        }
      } else if (e.key === "ArrowLeft" || e.key === "ArrowUp") {
        e.preventDefault();
        const newSection = Math.max(activeSection - 1, 0);
        if (newSection !== activeSection) {
          setIsScrolling(true);
          scrollToSection(newSection);
          setTimeout(() => setIsScrolling(false), 800);
        }
      }
    };

    window.addEventListener("keydown", handleKeyDown);

    return () => {
      window.removeEventListener("keydown", handleKeyDown);
    };
  }, [activeSection, isScrolling]);

  // Make first section visible on initial load
  useEffect(() => {
    if (sectionRefs.current[0]) {
      sectionRefs.current[0].classList.add(styles.visible);
    }
  }, []);

  return (
    <div className={styles.howItWorksContainer}>
      <h1 className={styles.mainTitle}>How It Works</h1>

      <div className={styles.navigationDots}>
        {[0, 1, 2].map((index) => (
          <div
            key={index}
            className={`${styles.dot} ${
              activeSection === index ? styles.activeDot : ""
            }`}
            onClick={() => !isScrolling && scrollToSection(index)}
          />
        ))}
      </div>

      <div className={styles.scrollHelp}>
        <span>Scroll or swipe to navigate</span>
      </div>

      <div
        className={styles.scrollContainer}
        ref={containerRef}
        onTouchStart={handleTouchStart}
        onTouchMove={handleTouchMove}
        onTouchEnd={handleTouchEnd}
      >
        <section
          ref={(el) => (sectionRefs.current[0] = el)}
          className={`${styles.scrollSection} ${styles.section1}`}
        >
          <div className={styles.contentBox}>
            <div className={styles.textContent}>
              <h2 className={styles.sectionTitle}>
                Create Your Virtual Fit Profile
              </h2>
              <p className={styles.sectionDescription}>
                Upload four photos from different angles, and Fit-On will create
                an accurate
                <strong> 3D model of your body</strong> using LiDAR technology.
                Get size-perfect outfit recommendations effortlessly!
              </p>
            </div>
            <div className={styles.imageContainer}>
              <img
                src="/img/virtual-profile.webp"
                alt="Create Virtual Profile"
                className={styles.sectionImage}
              />
            </div>
          </div>
          <div
            className={styles.scrollIndicator}
            onClick={() => !isScrolling && scrollToSection(1)}
          >
            <span className={styles.scrollText}>Next Step</span>
            <div className={styles.scrollArrow}></div>
          </div>
        </section>

        <section
          ref={(el) => (sectionRefs.current[1] = el)}
          className={`${styles.scrollSection} ${styles.section2}`}
        >
          <div className={styles.contentBox}>
            <div className={styles.textContent}>
              <h2 className={styles.sectionTitle}>
                Explore & Try On Digitally
              </h2>
              <p className={styles.sectionDescription}>
                Browse your <strong>digital wardrobe</strong> or shop from
                partnered brands. Mix and match outfits to see how they fit on
                your virtual profile—
                <strong>no more returns due to wrong sizing!</strong>
              </p>
            </div>
            <div className={styles.imageContainer}>
              <img
                src="/img/try-on-digitally.webp"
                alt="Try On Digitally"
                className={styles.sectionImage}
              />
            </div>
          </div>
          <div
            className={styles.scrollIndicator}
            onClick={() => !isScrolling && scrollToSection(2)}
          >
            <span className={styles.scrollText}>Next Step</span>
            <div className={styles.scrollArrow}></div>
          </div>
        </section>

        <section
          ref={(el) => (sectionRefs.current[2] = el)}
          className={`${styles.scrollSection} ${styles.section3}`}
        >
          <div className={styles.contentBox}>
            <div className={styles.textContent}>
              <h2 className={styles.sectionTitle}>Shop with Confidence</h2>
              <p className={styles.sectionDescription}>
                Once you find the perfect outfit, purchase directly through the
                app. Whether online or in-store, Fit-On ensures{" "}
                <strong>a smooth and hassle-free shopping experience!</strong>
              </p>
            </div>
            <div className={styles.imageContainer}>
              <img
                src="/img/shop-confidence.webp"
                alt="Shop with Confidence"
                className={styles.sectionImage}
              />
            </div>
          </div>
          <div className={styles.ctaContainer}>
            <button className={`${styles.ctaButton} ${styles.iosButton}`}>
              <span className={styles.buttonIcon}>📱</span>
              Download iOS App
            </button>
            <button className={`${styles.ctaButton} ${styles.webButton}`}>
              <span className={styles.buttonIcon}>🌐</span>
              Start with Web Version
            </button>
          </div>
        </section>
      </div>
    </div>
  );
};

export default HowItWorks;
