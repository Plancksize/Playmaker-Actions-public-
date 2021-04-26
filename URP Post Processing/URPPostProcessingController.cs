//Playmaker Actions by Plancksize

using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Post Processing")]
    [Tooltip("Controller Set for URP Post Effects (Volume)")]

    public class URPPostProcessingController : FsmStateActionAdvanced
    {
        //Enums
        #region ENUMS
        public enum volumeOverrides
        {
            None,
            Bloom,
            ChannelMixer,
            ChromaticAberration,
            ColorAdjustments,
            ColorLookup,
            DepthOfField,
            FilmGrain,
            LensDistortion,
            LiftGammaGain,
            MotionBlur,
            PaniniProjection,
            ShadowsMidtonesHighlights,
            SplitToning,
            Vignette,
            WhiteBalance
        }
        #endregion

        //Show & Hide Switches
        #region SWITCH VARIABLES
        public bool HideActiveAndDefault() { if (effect != volumeOverrides.None) return false; else return true; }
        public bool HideVolume() { if (selectedVolumeProfile == null) return false; else return true; }
        public bool HideBloom() { if (effect == volumeOverrides.Bloom) return false; else return true; }
        public bool HideChannelMixer() { if (effect == volumeOverrides.ChannelMixer) return false; else return true; }
        public bool HideChromaticAberration() { if (effect == volumeOverrides.ChromaticAberration) return false; else return true; }
        public bool HideColorAdjustments() { if (effect == volumeOverrides.ColorAdjustments) return false; else return true; }
        public bool HideColorLookup() { if (effect == volumeOverrides.ColorLookup) return false; else return true; }
        public bool HideDepthOfField() { if (effect == volumeOverrides.DepthOfField) return false; else return true; }
        public bool HideFilmGrain() { if (effect == volumeOverrides.FilmGrain) return false; else return true; }
        public bool HideLensDistortion() { if (effect == volumeOverrides.LensDistortion) return false; else return true; }
        public bool HideLiftGammaGain() { if (effect == volumeOverrides.LiftGammaGain) return false; else return true; }
        public bool HideMotionBlur() { if (effect == volumeOverrides.MotionBlur) return false; else return true; }
        public bool HidePaniniProjection() { if (effect == volumeOverrides.PaniniProjection) return false; else return true; }
        public bool HideShadowsMidtonesHighlights() { if (effect == volumeOverrides.ShadowsMidtonesHighlights) return false; else return true; }
        public bool HideSplitToning() { if (effect == volumeOverrides.SplitToning) return false; else return true; }
        public bool HideVignette() { if (effect == volumeOverrides.Vignette) return false; else return true; }
        public bool HideWhiteBalance() { if (effect == volumeOverrides.WhiteBalance) return false; else return true; }
        #endregion

        //Variables
        #region SETUP VARIABLES
        [ActionSection("Setup")]

        [HideIf("HideVolume")]
        [Title("Volume")]
        [CheckForComponent(typeof(Volume))]
        [Tooltip("Will not be changed during Runtime")]
        public FsmOwnerDefault volumeOwner;

        [Title("Volume Profile")]
        [Tooltip("Will not be changed during Runtime")]
        public VolumeProfile selectedVolumeProfile;

        //      There is no real need for this option. This setting should be part of the manual Volume setup
        //      
        //      [Title("Global Mode")]
        //      [Tooltip("Will not be changed during Runtime")]
        //      public FsmBool global;

        [HideIf("HideVolume")]
        [Title("Weight")]
        [HasFloatSlider(0, 1)]
        public FsmFloat volumeWeight;
        [HideIf("HideVolume")]
        [Title("Priority")]
        public FsmFloat volumePriority;

        [ActionSection("Effects")]
        [Title("Effects")]
        public volumeOverrides effect;

        [HideIf("HideActiveAndDefault")]
        [Title("Active")]
        public FsmBool fxActive;

        [ActionSection("")]

        [HideIf("HideActiveAndDefault")]
        [Tooltip("Sets this effect values to Unity's defaults./nWill not work in Playmode.")]
        [Title("Set Effect to Default")]
        public bool setDefault = false;

        #endregion

        #region BLOOM VARIABLES
        [HideIf("HideBloom")]
        [ActionSection("Bloom")]
        [Title("Threshold")]
        public FsmFloat bloomThreshold;

        [Title("Intensity")]
        [HideIf("HideBloom")]
        public FsmFloat bloomIntensity;

        [Title("Scatter")]
        [HideIf("HideBloom")]
        [HasFloatSlider(0, 1)]
        public FsmFloat bloomScatter;

        [Title("Tint")]
        [HideIf("HideBloom")]
        public FsmColor bloomTint;

        [Title("Clamp")]
        [HideIf("HideBloom")]
        public FsmFloat bloomClamp;

        [Title("HQ Filtering")]
        [HideIf("HideBloom")]
        public FsmBool bloomHQFiltering;

        [Title("HQ Filtering")]
        [HideIf("HideBloom")]
        [HasIntSlider(0, 16)]
        public FsmInt bloomSkipIterations;

        [ActionSection("Effects")]
        [Title("Dirt Texture")]
        [HideIf("HideBloom")]
        public FsmTexture bloomDirtTexture;

        [Title("Dirt Intensity")]
        [HideIf("HideBloom")]
        public FsmFloat bloomDirtIntensity;
        #endregion

        #region CHANNEL MIXER VARIABLES
        [HideIf("HideChannelMixer")]
        [ActionSection("Channel Mixer - RED")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat redRed;
        [HideIf("HideChannelMixer")]

        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat redGreen;
        [HideIf("HideChannelMixer")]

        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat redBlue;

        [HideIf("HideChannelMixer")]
        [ActionSection("Channel Mixer - GREEN")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat greenRed;

        [HideIf("HideChannelMixer")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat greenGreen;

        [HideIf("HideChannelMixer")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat greendBlue;

        [HideIf("HideChannelMixer")]
        [ActionSection("Channel Mixer - BLUE")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat blueRed;

        [HideIf("HideChannelMixer")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat blueGreen;

        [HideIf("HideChannelMixer")]
        [HasFloatSlider(0, 200)]
        [Title("")]
        public FsmFloat blueBlue;

        #endregion

        #region CHROMATIC ABERRATION VARIABLES
        [HideIf("HideColorAdjustments")]
        [ActionSection("Color Adjustments")]
        [Title("Post Exposure")]
        public FsmFloat colorAdjustmentsExposure;

        [HideIf("HideColorAdjustments")]
        [HasFloatSlider(-100, 100)]
        [Title("Contrast")]
        public FsmFloat colorAdjustmentsContrast;

        [HideIf("HideColorAdjustments")]
        [Title("Color Filter")]
        public FsmColor colorAdjustmentsFilter;

        [HideIf("HideColorAdjustments")]
        [HasFloatSlider(-180, 180)]
        [Title("Hue Shift")]
        public FsmFloat colorAdjustmentsHueShift;

        [HideIf("HideColorAdjustments")]
        [HasFloatSlider(-100, 100)]
        [Title("Saturation")]
        public FsmFloat colorAdjustmentsSaturation;

        #endregion

        #region COLOR ADJUSTMENTS VARIABLES
        [HideIf("HideChromaticAberration")]
        [ActionSection("Channel Mixer")]
        [HasFloatSlider(0, 1)]
        [Title("Intensity")]
        public FsmFloat chromaticAberrationIntensity;

        #endregion

        #region COLOR LOOKUP VARIABLES

        [HideIf("HideColorLookup")]
        [ActionSection("Color Lookup")]
        [Title("Lookup Texture")]
        public FsmTexture colorLookuplookupTexture;

        [HideIf("HideColorLookup")]
        [HasFloatSlider(0, 1)]
        [Title("Contribution")]
        public FsmFloat colorLookupContribution;

        #endregion

        #region DEPTH OF FIELD VARIABLES

        [HideIf("HideDepthOfField")]
        [ActionSection("Depth of Field")]
        [Title("Mode")]
        public DepthOfFieldMode depthMode;

        //switch
        public bool HideDepthModeGaussian() { if (effect == volumeOverrides.DepthOfField && depthMode == DepthOfFieldMode.Gaussian) return false; else return true; }

        [HideIf("HideDepthModeGaussian")]
        [ActionSection("Gaussian")]
        [Title("Start")]
        public FsmFloat gaussianStart;

        [HideIf("HideDepthModeGaussian")]
        [Title("Start")]
        public FsmFloat gaussianEnd;

        [HideIf("HideDepthModeGaussian")]
        [Title("Max Radius")]
        [HasFloatSlider(0.5f, 1.5f)]
        public FsmFloat gaussianMaxRadius;

        [HideIf("HideDepthModeGaussian")]
        [Title("HQ Sampling")]
        public FsmBool gaussianHQSampling;

        //switch
        public bool HideDepthModeBokeh() { if (effect == volumeOverrides.DepthOfField && depthMode == DepthOfFieldMode.Bokeh) return false; else return true; }

        [HideIf("HideDepthModeBokeh")]
        [Title("Focus Distance")]
        public FsmFloat bokehFocusDistance;

        [HideIf("HideDepthModeBokeh")]
        [Title("Focal Lenght")]
        [HasIntSlider(1, 300)]
        public FsmInt bokehFocalLenght;

        [HideIf("HideDepthModeBokeh")]
        [Title("Aperture")]
        [HasFloatSlider(1, 32)]
        public FsmFloat bokehAperture;

        [HideIf("HideDepthModeBokeh")]
        [Title("Blade Count")]
        [HasIntSlider(3, 9)]
        public FsmInt bokehBladeCount;

        [HideIf("HideDepthModeBokeh")]
        [Title("Blade Curvature")]
        [HasFloatSlider(0, 1)]
        public FsmFloat bokehBladeCurvature;

        [HideIf("HideDepthModeBokeh")]
        [Title("Blade Rotation")]
        [HasIntSlider(-180, 180)]
        public FsmInt bokehBladeRotation;
        #endregion

        #region FILM GRAIN VARIABLES

        [HideIf("HideFilmGrain")]
        [ActionSection("Film Grain")]
        [Title("Type")]
        public FilmGrainLookup filmGrainType;

        [HideIf("HideFilmGrain")]
        [Title("Intensity")]
        [HasFloatSlider(0, 1)]
        public FsmFloat filmGrainIntensity;

        [HideIf("HideFilmGrain")]
        [Title("Response")]
        [HasFloatSlider(0, 1)]
        public FsmFloat filmGrainResponse;

        //switch
        public bool HideFilmGrainCustom() { if (effect == volumeOverrides.FilmGrain && filmGrainType == FilmGrainLookup.Custom) return false; else return true; }
        [HideIf("HideFilmGrainCustom")]
        [Title("Texture")]
        public FsmTexture filmGrainTexture;

        #endregion

        #region LENS DISTORTION VARIABLES
        [HideIf("HideLensDistortion")]
        [ActionSection("Lens Distortion")]
        [Title("Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat lensDistortionIntensity;

        [Title("X Multiplier")]
        [HideIf("HideLensDistortion")]
        [HasFloatSlider(0, 1)]
        public FsmFloat lensDistortionXMult;

        [Title("Y Multiplier")]
        [HideIf("HideLensDistortion")]
        [HasFloatSlider(0, 1)]
        public FsmFloat lensDistortionYMult;

        [Title("Center")]
        [HideIf("HideLensDistortion")]
        public FsmVector2 lensDistortionCenter;

        [Title("Scale")]
        [HideIf("HideLensDistortion")]
        [HasFloatSlider(0.01f, 5)]
        public FsmFloat lensDistortionScale;
        #endregion

        #region LIFT GAMMA GAIN VARIABLES
        [HideIf("HideLiftGammaGain")]
        [ActionSection("Lift Gamma Gain")]
        [Title("Lift")]
        public FsmColor lggLift;

        [HideIf("HideLiftGammaGain")]
        [Title("Lift Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat lggLiftIntensity;

        [HideIf("HideLiftGammaGain")]
        [Title("Gamma")]
        public FsmColor lggGamma;

        [HideIf("HideLiftGammaGain")]
        [Title("Gamm Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat lggGammaIntensity;

        [HideIf("HideLiftGammaGain")]
        [Title("Gain")]
        public FsmColor lggGain;

        [HideIf("HideLiftGammaGain")]
        [Title("Gain Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat lggGainIntensity;
        #endregion

        #region MOTION BLUR VARIABLES

        [HideIf("HideMotionBlur")]
        [ActionSection("Motion Blur")]
        [Title("Mode")]
        public MotionBlurMode motionBlurMode;

        [HideIf("HideMotionBlur")]
        [Title("Quality")]
        public MotionBlurQuality motionBlurQuality;

        [HideIf("HideMotionBlur")]
        [Title("Intensity")]
        [HasFloatSlider(0, 1)]
        public FsmFloat motionBlurIntensity;

        [HideIf("HideMotionBlur")]
        [Title("Clamp")]
        [HasFloatSlider(0, 0.2f)]
        public FsmFloat motionBlurClamp;
        #endregion

        #region PANINI PROJECTION VARIABLES
        [HideIf("HidePaniniProjection")]
        [ActionSection("Panini Projection")]
        [Title("Distance")]
        [HasFloatSlider(0, 1)]
        public FsmFloat paniniDistance;

        [HideIf("HidePaniniProjection")]
        [Title("Crop to Fit")]
        [HasFloatSlider(0, 1)]
        public FsmFloat paniniCrop;
        #endregion

        #region SHADOWS MIDTONES HIGHLIGHTS VARIABLES
        [HideIf("HideShadowsMidtonesHighlights")]
        [ActionSection("Lift Gamma Gain")]
        [Title("Shadows")]
        public FsmColor smhShadows;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Shadows Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat smhShadowsIntensity;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Midtones")]
        public FsmColor smhMidtones;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Midtones Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat smhMidtonesIntensity;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Highlights")]
        public FsmColor smhHighlights;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("Highlights Intensity")]
        [HasFloatSlider(-1, 1)]
        public FsmFloat smhHighlightsIntensity;

        [HideIf("HideShadowsMidtonesHighlights")]
        [ActionSection("Shadow Limits")]
        [Title("Start")]
        public FsmFloat smhShadowLimitStart;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("End")]
        public FsmFloat smhShadowLimitEnd;

        [HideIf("HideShadowsMidtonesHighlights")]
        [ActionSection("Highlights Limits")]
        [Title("Start")]
        public FsmFloat smhHighlightLimitStart;

        [HideIf("HideShadowsMidtonesHighlights")]
        [Title("End")]
        public FsmFloat smhHighlightLimitEnd;
        #endregion

        #region SPLIT TONING VARIABLES
        [HideIf("HideSplitToning")]
        [ActionSection("Split Toning")]
        [Title("Shadows")]
        public FsmColor splitToningShadows;

        [HideIf("HideSplitToning")]
        [Title("Highlights")]
        public FsmColor splitToningHighlights;

        [HideIf("HideSplitToning")]
        [Title("Highlights")]
        [HasIntSlider(-100, 100)]
        public FsmInt splitToningBalance;
        #endregion

        #region VIGNETTE VARIABLES
        [HideIf("HideVignette")]
        [ActionSection("Vignette")]
        [Title("Color")]
        public FsmColor vignetteColor;

        [HideIf("HideVignette")]
        [Title("Center")]
        public FsmVector2 vignetteCenter;

        [HideIf("HideVignette")]
        [Title("Intensity")]
        [HasFloatSlider(0, 1)]
        public FsmFloat vignetteIntensity;

        [HideIf("HideVignette")]
        [Title("Smoothness")]
        [HasFloatSlider(0, 1)]
        public FsmFloat vignetteSmoothness;

        [HideIf("HideVignette")]
        [Title("Rounded")]
        public FsmBool vignetteRounded;

        #endregion

        #region WHITE BALANCE VARIABLES
        [HideIf("HideWhiteBalance")]
        [ActionSection("White Balance")]
        [Title("Temperature")]
        [HasIntSlider(-100, 100)]
        public FsmInt wbTemperatere;

        [HideIf("HideWhiteBalance")]
        [Title("Tint")]
        [HasIntSlider(-100, 100)]
        public FsmInt wbTint;
        #endregion

        #region PRIVATE VARIABLES
        private GameObject volumeObj; 
        private Volume volume;
        private VolumeProfile volumeProfile;
        #endregion

        //Main

        #region ERRORCHECK()
        public override string ErrorCheck()
        {

            volumeObj = Fsm.GetOwnerDefaultTarget(volumeOwner);
            if (volumeObj != null && !volumeObj.TryGetComponent(out Volume test))
                return ("");
            else if (volumeObj == null && selectedVolumeProfile == null)
                return ("A GameObject with a Volume Component or a Volume Profile is Required. (Current GameObject Value and Volume Profile is Null)");
            else if (selectedVolumeProfile == null && !volumeObj.TryGetComponent(out Volume vol))
                return ("A GameObject with a Volume Component or a Volume Profile is Required  (Couldn't Find a Volume Component on the GameObject.");
            else if (volumeObj.GetComponent<Volume>().sharedProfile == null)
                return ("Volume Profile is Required on the selected Volume  (Couldn't Find a Volume Component on the GameObject.");

            if (selectedVolumeProfile == null)
            {
                volumeObj = Fsm.GetOwnerDefaultTarget(volumeOwner);
                volume = volumeObj.GetComponent<Volume>();
                volumeProfile = volumeObj.GetComponent<Volume>().sharedProfile;
            }
            else volumeProfile = selectedVolumeProfile;

            if (!Application.isPlaying)
            {
                if (setDefault)
                {
                    SetDefault(effect);
                }
            }

            #region OVERRIDE CHECK
            if (effect == volumeOverrides.Bloom)
                if (!volumeProfile.TryGet<Bloom>(out var bloom))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.ChannelMixer)
                if (!volumeProfile.TryGet<ChannelMixer>(out var channelMixer))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.ChromaticAberration)
                if (!volumeProfile.TryGet<ChromaticAberration>(out var chromaticAberration))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.ColorAdjustments)
                if (!volumeProfile.TryGet<ColorAdjustments>(out var colorAdjustments))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.ColorLookup)
                if (!volumeProfile.TryGet<ColorLookup>(out var colorLookup))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.DepthOfField)
                if (!volumeProfile.TryGet<DepthOfField>(out var depthOfField))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.FilmGrain)
                if (!volumeProfile.TryGet<FilmGrain>(out var filmGrain))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.LensDistortion)
                if (!volumeProfile.TryGet<LensDistortion>(out var lensDistortion))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.LiftGammaGain)
                if (!volumeProfile.TryGet<LiftGammaGain>(out var liftGammaGain))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.MotionBlur)
                if (!volumeProfile.TryGet<MotionBlur>(out var motionBlur))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.PaniniProjection)
                if (!volumeProfile.TryGet<PaniniProjection>(out var paniniProjection))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.ShadowsMidtonesHighlights)
                if (!volumeProfile.TryGet<ShadowsMidtonesHighlights>(out var shadowsMidtonesHighlights))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.SplitToning)
                if (!volumeProfile.TryGet<SplitToning>(out var splitToning))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.Vignette)
                if (!volumeProfile.TryGet<Vignette>(out var vignette))
                     return ("No " + effect + " Override found on Volume Profile."); 
            if (effect == volumeOverrides.WhiteBalance)
                if (!volumeProfile.TryGet<WhiteBalance>(out var whiteBalance))
                     return ("No " + effect + " Override found on Volume Profile."); 
            #endregion
            
            return "";
        }
        #endregion

        #region RESET()
        public override void Reset()
        {

            #region SETUP RESET
            everyFrame = false;
            setDefault = false;
            volume = null;
            volumeProfile = null;
            //global = true;
            volumeWeight = 1;
            volumePriority = 0;
            fxActive = true;
            #endregion

            #region BLOOM RESET
            bloomThreshold = new FsmFloat { UseVariable = true };
            bloomIntensity = new FsmFloat { UseVariable = true };
            bloomScatter = new FsmFloat { UseVariable = true };
            bloomTint = new FsmColor { UseVariable = true };
            bloomClamp = new FsmFloat { UseVariable = true };
            bloomHQFiltering = new FsmBool { UseVariable = true };
            bloomSkipIterations = new FsmInt { UseVariable = true };
            bloomDirtTexture = new FsmTexture { UseVariable = true };
            bloomDirtIntensity = new FsmFloat { UseVariable = true };
            #endregion

            #region CHANNEL MIXER RESET
            redRed = new FsmFloat { UseVariable = true };
            redGreen = new FsmFloat { UseVariable = true };
            redBlue = new FsmFloat { UseVariable = true };
            greenRed = new FsmFloat { UseVariable = true };
            greenGreen = new FsmFloat { UseVariable = true };
            greendBlue = new FsmFloat { UseVariable = true };
            blueRed = new FsmFloat { UseVariable = true };
            blueGreen = new FsmFloat { UseVariable = true };
            blueBlue = new FsmFloat { UseVariable = true };
            #endregion

            #region CHROMATIC ABERRATION RESET
            chromaticAberrationIntensity = new FsmFloat { UseVariable = true };
            #endregion

            #region COLOR ADJUSTMENTS RESET
            colorAdjustmentsExposure = new FsmFloat { UseVariable = true };
            colorAdjustmentsContrast = new FsmFloat { UseVariable = true };
            colorAdjustmentsFilter = new FsmColor { UseVariable = true };
            colorAdjustmentsHueShift = new FsmFloat { UseVariable = true };
            colorAdjustmentsSaturation = new FsmFloat { UseVariable = true };
            #endregion

            #region COLOR LOOKUP RESET
            colorLookuplookupTexture = new FsmTexture { UseVariable = true };
            colorLookupContribution = new FsmFloat { UseVariable = true };
            #endregion

            #region DEPTH OF FIELD RESET
            depthMode = DepthOfFieldMode.Off;
            gaussianStart = new FsmFloat { UseVariable = true };
            gaussianEnd = new FsmFloat { UseVariable = true };
            gaussianMaxRadius = new FsmFloat { UseVariable = true };
            gaussianHQSampling = new FsmBool { UseVariable = true };

            bokehFocusDistance = new FsmFloat { UseVariable = true };
            bokehFocalLenght = new FsmInt { UseVariable = true };
            bokehAperture = new FsmFloat { UseVariable = true };
            bokehBladeCount = new FsmInt { UseVariable = true };
            bokehBladeCurvature = new FsmFloat { UseVariable = true };
            bokehBladeRotation = new FsmInt { UseVariable = true };
            #endregion

            #region FILM GRAIN RESET
            filmGrainType = FilmGrainLookup.Thin1;
            filmGrainIntensity = new FsmFloat { UseVariable = true };
            filmGrainResponse = new FsmFloat { UseVariable = true };
            filmGrainTexture = new FsmTexture { UseVariable = true };
            #endregion

            #region LENS DISTORTION RESET
            lensDistortionIntensity = new FsmFloat { UseVariable = true };
            lensDistortionXMult = new FsmFloat { UseVariable = true };
            lensDistortionYMult = new FsmFloat { UseVariable = true };
            lensDistortionCenter = new FsmVector2 { UseVariable = true };
            lensDistortionScale = new FsmFloat { UseVariable = true };
            #endregion

            #region LIFT GAMMA GAIN RESET
            lggLift = new FsmColor { UseVariable = true };
            lggLiftIntensity = new FsmFloat { UseVariable = true };
            lggGamma = new FsmColor { UseVariable = true };
            lggGammaIntensity = new FsmFloat { UseVariable = true };
            lggGain = new FsmColor { UseVariable = true };
            lggGainIntensity = new FsmFloat { UseVariable = true };
            #endregion

            #region MOTION BLUR RESET
            motionBlurMode = MotionBlurMode.CameraAndObjects;
            motionBlurQuality = MotionBlurQuality.Low;
            motionBlurIntensity = new FsmFloat { UseVariable = true };
            motionBlurClamp = new FsmFloat { UseVariable = true };
            #endregion

            #region PANINI PROJECTION RESET
            paniniDistance = new FsmFloat { UseVariable = true };
            paniniCrop = new FsmFloat { UseVariable = true };
            #endregion

            #region SHADOWS MIDTONES HIGHLIGHTS RESET
            smhShadows = new FsmColor { UseVariable = true };
            smhShadowsIntensity = new FsmFloat { UseVariable = true };
            smhMidtones = new FsmColor { UseVariable = true };
            smhMidtonesIntensity = new FsmFloat { UseVariable = true };
            smhHighlights = new FsmColor { UseVariable = true };
            smhHighlightsIntensity = new FsmFloat { UseVariable = true };
            smhShadowLimitStart = new FsmFloat { UseVariable = true };
            smhShadowLimitEnd = new FsmFloat { UseVariable = true };
            smhHighlightLimitStart = new FsmFloat { UseVariable = true };
            smhHighlightLimitEnd = new FsmFloat { UseVariable = true };
            #endregion

            #region SPLIT TONING RESET
            splitToningShadows = new FsmColor { UseVariable = true };
            splitToningHighlights = new FsmColor { UseVariable = true };
            splitToningBalance = new FsmInt { UseVariable = true };
            #endregion

            #region VIGNETTE RESET
            vignetteColor = new FsmColor { UseVariable = true };
            vignetteCenter = new FsmVector2 { UseVariable = true };
            vignetteIntensity = new FsmFloat { UseVariable = true };
            vignetteSmoothness = new FsmFloat { UseVariable = true };
            vignetteRounded = new FsmBool { UseVariable = true };
            #endregion

            #region WHITE BALANCE RESET
            wbTemperatere = new FsmInt { UseVariable = true };
            wbTint = new FsmInt { UseVariable = true };
            #endregion

        }
        #endregion

        #region ENTER() and UPDATE()
        public override void OnEnter()
        {
            volumeObj = Fsm.GetOwnerDefaultTarget(volumeOwner);

            #region RUNTIME ERROR CHECK LOGGING & BREAK

            if (volumeObj != null && !volumeObj.TryGetComponent(out Volume test) && selectedVolumeProfile != null)
            {
                volumeProfile = selectedVolumeProfile;
            }
            else if (volumeObj == null && selectedVolumeProfile == null)
            {
                LogWarning("There is no GameObject with Volume component or a Volume Profile selected" + " @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }
            else if (volumeObj != null && !volumeObj.TryGetComponent(out Volume vol) && selectedVolumeProfile == null)
            {
                LogWarning ("There is no Volume component on [" + volumeObj.name + "] @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }
            else if (volumeObj != null && volumeObj.GetComponent<Volume>().sharedProfile == null && selectedVolumeProfile == null)
            {
                LogWarning("There is no profile in the selected Volume ["  + volumeObj.name + "] @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }
            else if (volumeObj != null && volumeObj.GetComponent<Volume>().sharedProfile != null && selectedVolumeProfile == null)
            {
                selectedVolumeProfile = volumeObj.GetComponent<Volume>().sharedProfile;
            }

            #endregion

            //volume.isGlobal = global.Value;

            Action();

            if (!everyFrame) Finish();
        }

        //On Update
        public override void OnActionUpdate()
        {
            Action();
        }
        #endregion

        #region ACTION()
        public void Action()
        {
            if (selectedVolumeProfile == null)
            {
                volume = volumeObj.GetComponent<Volume>();
                volumeProfile = volumeObj.GetComponent<Volume>().sharedProfile;                
            }
            else volumeProfile = selectedVolumeProfile;

            if (selectedVolumeProfile == null)
            {
                volume.weight = volumeWeight.Value;
                volume.priority = volumePriority.Value;
            }

            if (setDefault)
            {
                LogWarning("This Option does not work in Playmode.");
                setDefault = false;
            }

            if (effect == volumeOverrides.Bloom) Bloom();
            if (effect == volumeOverrides.ChannelMixer) ChannelMixer();
            if (effect == volumeOverrides.ChromaticAberration) ChromaticAberration();
            if (effect == volumeOverrides.ColorAdjustments) ColorAdjustments();
            if (effect == volumeOverrides.ColorLookup) ColorLookup();
            if (effect == volumeOverrides.DepthOfField) DepthOfField();
            if (effect == volumeOverrides.FilmGrain) FilmGrain();
            if (effect == volumeOverrides.LensDistortion) LensDistortion();
            if (effect == volumeOverrides.LiftGammaGain) LiftGammaGain();
            if (effect == volumeOverrides.MotionBlur) MotionBlur();
            if (effect == volumeOverrides.PaniniProjection) PaniniProjection();
            if (effect == volumeOverrides.ShadowsMidtonesHighlights) ShadowsMidtonesHighlights();
            if (effect == volumeOverrides.SplitToning) SplitToning();
            if (effect == volumeOverrides.Vignette) Vignette();
            if (effect == volumeOverrides.WhiteBalance) WhiteBalance();
        }
        #endregion

        //Effects

        #region BLOOM()
        public void Bloom()
        {
            if (!volumeProfile.TryGet<Bloom>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }
                
            volumeProfile.TryGet<Bloom>(out var bloom);

            bloom.active = fxActive.Value;
            if (!bloom.active) return;
            else

            if (!bloomThreshold.IsNone)
            {
                bloom.threshold.overrideState = true;
                bloom.threshold.value = bloomThreshold.Value;
            }                
            else 
                bloom.threshold.overrideState = false;

            if (!bloomIntensity.IsNone)
                { 
                    bloom.intensity.overrideState = true; 
                    bloom.intensity.value = bloomIntensity.Value; 
                }
            else bloom.intensity.overrideState = false;

            if (!bloomScatter.IsNone)
            {
                bloom.scatter.overrideState = true;
                bloom.scatter.value = bloomScatter.Value;
            }
            else bloom.scatter.overrideState = false;

            if (!bloomTint.IsNone)
            {
                bloom.tint.overrideState = true;
                bloom.tint.value = bloomTint.Value;
            }
            else bloom.tint.overrideState = false;

            if (!bloomClamp.IsNone)
            {
                bloom.clamp.overrideState = true;
                bloom.clamp.value = bloomClamp.Value;
            }
            else bloom.clamp.overrideState = false;

            if (!bloomHQFiltering.IsNone)
            {
                bloom.highQualityFiltering.overrideState = true;
                bloom.highQualityFiltering.value = bloomHQFiltering.Value;
            }
            else bloom.highQualityFiltering.overrideState = false;

            if (!bloomSkipIterations.IsNone)
            {
                bloom.skipIterations.overrideState = true;
                bloom.skipIterations.value = bloomSkipIterations.Value;
            }
            else bloom.skipIterations.overrideState = false;

            if (!bloomDirtTexture.IsNone)
            {
                bloom.dirtTexture.overrideState = true;
                bloom.dirtTexture.value = bloomDirtTexture.Value;
            }
            else bloom.dirtTexture.overrideState = false;

            if (!bloomDirtIntensity.IsNone)
            {
                bloom.dirtIntensity.overrideState = true;
                bloom.dirtIntensity.value = bloomDirtIntensity.Value;
            }
            else bloom.dirtIntensity.overrideState = false;
        }
        #endregion

        #region CHANNEL MIXER()
        public void ChannelMixer()
        {
            if (!volumeProfile.TryGet<ChannelMixer>(out var overridetest))
            {
                LogWarning("No " + effect + " found on volume @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ChannelMixer>(out var mixer);

            mixer.active = fxActive.Value;
            if (!mixer.active) return;
            else

            if (!redRed.IsNone)
            {
                mixer.redOutRedIn.overrideState = true;
                mixer.redOutRedIn.value = redRed.Value;
            }
            else mixer.redOutRedIn.overrideState = false;

            if (!redGreen.IsNone)
            {
                mixer.redOutGreenIn.overrideState = true;
                mixer.redOutGreenIn.value = redGreen.Value;
            }
            else mixer.redOutGreenIn.overrideState = false;

            if (!redBlue.IsNone)
            {
                mixer.redOutBlueIn.overrideState = true;
                mixer.redOutBlueIn.value = redBlue.Value;
            }
            else mixer.redOutBlueIn.overrideState = false;

            if (!greenRed.IsNone)
            {
                mixer.greenOutRedIn.overrideState = true;
                mixer.greenOutRedIn.value = greenRed.Value;
            }
            else mixer.greenOutRedIn.overrideState = false;

            if (!greenGreen.IsNone)
            {
                mixer.greenOutGreenIn.overrideState = true;
                mixer.greenOutGreenIn.value = greenGreen.Value;
            }
            else mixer.greenOutGreenIn.overrideState = false;

            if (!greendBlue.IsNone)
            {
                mixer.greenOutBlueIn.overrideState = true;
                mixer.greenOutBlueIn.value = greendBlue.Value;
            }
            else mixer.greenOutBlueIn.overrideState = false;

            if (!blueRed.IsNone)
            {
                mixer.blueOutRedIn.overrideState = true;
                mixer.blueOutRedIn.value = blueRed.Value;
            }
            else mixer.blueOutRedIn.overrideState = false;

            if (!blueGreen.IsNone)
            {
                mixer.blueOutGreenIn.overrideState = true;
                mixer.blueOutGreenIn.value = blueGreen.Value;
            }
            else mixer.blueOutGreenIn.overrideState = false;

            if (!blueBlue.IsNone)
            {
                mixer.blueOutBlueIn.overrideState = true;
                mixer.blueOutBlueIn.value = blueBlue.Value;
            }
            else mixer.blueOutBlueIn.overrideState = false;
        }
        #endregion

        #region CHORMATIC ABERRATION()
        public void ChromaticAberration()
        {
            if (!volumeProfile.TryGet<ChromaticAberration>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ChromaticAberration>(out var chromaticAberration);

            chromaticAberration.active = fxActive.Value;
            if (!chromaticAberration.active) return;
            else

            if (!chromaticAberrationIntensity.IsNone)
            {
                chromaticAberration.intensity.overrideState = true;
                chromaticAberration.intensity.value = chromaticAberrationIntensity.Value;
            }
            else chromaticAberration.intensity.overrideState = false;
        }
        #endregion

        #region COLOR ADJUSTMENTS()
        public void ColorAdjustments()
        {
            if (!volumeProfile.TryGet<ColorAdjustments>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ColorAdjustments>(out var colorAdjustments);

            colorAdjustments.active = fxActive.Value;
            if (!colorAdjustments.active) return;
            else

            if (!colorAdjustmentsExposure.IsNone)
            {
                colorAdjustments.postExposure.overrideState = true;
                colorAdjustments.postExposure.value = colorAdjustmentsExposure.Value;
            }
            else colorAdjustments.postExposure.overrideState = false;

            if (!colorAdjustmentsContrast.IsNone)
            {
                colorAdjustments.contrast.overrideState = true;
                colorAdjustments.contrast.value = colorAdjustmentsContrast.Value;
            }
            else colorAdjustments.contrast.overrideState = false;

            if (!colorAdjustmentsFilter.IsNone)
            {
                colorAdjustments.colorFilter.overrideState = true;
                colorAdjustments.colorFilter.value = colorAdjustmentsFilter.Value;
            }
            else colorAdjustments.colorFilter.overrideState = false;

            if (!colorAdjustmentsHueShift.IsNone)
            {
                colorAdjustments.hueShift.overrideState = true;
                colorAdjustments.hueShift.value = colorAdjustmentsHueShift.Value;
            }
            else colorAdjustments.hueShift.overrideState = false;

            if (!colorAdjustmentsSaturation.IsNone)
            {
                colorAdjustments.saturation.overrideState = true;
                colorAdjustments.saturation.value = colorAdjustmentsSaturation.Value;
            }
            else colorAdjustments.saturation.overrideState = false;
        }
        #endregion

        #region COLOR LOOKUP()
        public void ColorLookup()
        {
            if (!volumeProfile.TryGet<ColorLookup>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ColorLookup>(out var colorLookup);

            colorLookup.active = fxActive.Value;
            if (!colorLookup.active) return;
            else

            if (!colorLookuplookupTexture.IsNone)
            {
                colorLookup.texture.overrideState = true;
                colorLookup.texture.value = colorLookuplookupTexture.Value;
            }
            else colorLookup.texture.overrideState = false;

            if (!colorLookupContribution.IsNone)
            {
                colorLookup.contribution.overrideState = true;
                colorLookup.contribution.value = colorLookupContribution.Value;
            }
            else colorLookup.contribution.overrideState = false;
        }
        #endregion

        #region DEPTH OF FIELD()
        public void DepthOfField()
        {
            if (!volumeProfile.TryGet<DepthOfField>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<DepthOfField>(out var depthOfField);

            depthOfField.active = fxActive.Value;
            if (!depthOfField.active) return;
            else

                depthOfField.mode.value = depthMode;

            if (depthMode != DepthOfFieldMode.Off)
            {
                depthOfField.mode.overrideState = true;
                depthOfField.mode.value = depthMode;
            }
            else depthOfField.mode.overrideState = false;

            if (depthMode == DepthOfFieldMode.Gaussian)
            {
                if (!gaussianStart.IsNone)
                {
                    depthOfField.gaussianStart.overrideState = true;
                    depthOfField.gaussianStart.value = gaussianStart.Value;
                }
                else depthOfField.gaussianStart.overrideState = false;

                if (!gaussianEnd.IsNone)
                {
                    depthOfField.gaussianEnd.overrideState = true;
                    depthOfField.gaussianEnd.value = gaussianEnd.Value;
                }
                else depthOfField.gaussianEnd.overrideState = false;

                if (!gaussianMaxRadius.IsNone)
                {
                    depthOfField.gaussianMaxRadius.overrideState = true;
                    depthOfField.gaussianMaxRadius.value = gaussianMaxRadius.Value;
                }
                else depthOfField.gaussianMaxRadius.overrideState = false;

                if (!gaussianHQSampling.IsNone)
                {
                    depthOfField.highQualitySampling.overrideState = true;
                    depthOfField.highQualitySampling.value = gaussianHQSampling.Value;
                }
                else depthOfField.highQualitySampling.overrideState = false;
            }
            else if (depthMode == DepthOfFieldMode.Bokeh)
            {
                if (!bokehFocusDistance.IsNone)
                {
                    depthOfField.focusDistance.overrideState = true;
                    depthOfField.focusDistance.value = bokehFocusDistance.Value;
                }
                else depthOfField.focusDistance.overrideState = false;

                if (!bokehFocalLenght.IsNone)
                {
                    depthOfField.focalLength.overrideState = true;
                    depthOfField.focalLength.value = bokehFocalLenght.Value;
                }
                else depthOfField.focalLength.overrideState = false;

                if (!bokehAperture.IsNone)
                {
                    depthOfField.aperture.overrideState = true;
                    depthOfField.aperture.value = bokehAperture.Value;
                }
                else depthOfField.aperture.overrideState = false;

                if (!bokehBladeCount.IsNone)
                {
                    depthOfField.bladeCount.overrideState = true;
                    depthOfField.bladeCount.value = bokehBladeCount.Value;
                }
                else depthOfField.bladeCount.overrideState = false;

                if (!bokehBladeCurvature.IsNone)
                {
                    depthOfField.bladeCurvature.overrideState = true;
                    depthOfField.bladeCurvature.value = bokehBladeCurvature.Value;
                }
                else depthOfField.bladeCurvature.overrideState = false;

                if (!bokehBladeRotation.IsNone)
                {
                    depthOfField.bladeRotation.overrideState = true;
                    depthOfField.bladeRotation.value = bokehBladeRotation.Value;
                }
                else depthOfField.bladeRotation.overrideState = false;
            }                
        }
        #endregion

        #region FILM GRAIN()
        public void FilmGrain()
        {
            if (!volumeProfile.TryGet<FilmGrain>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<FilmGrain>(out var filmGrain);

            filmGrain.active = fxActive.Value;

            if (!filmGrain.active) filmGrain.type.overrideState = false;
            else
            {
                filmGrain.type.overrideState = true;
                filmGrain.type.value = filmGrainType;
            } 

            if (!filmGrainIntensity.IsNone)
            {
                filmGrain.intensity.overrideState = true;
                filmGrain.intensity.value = filmGrainIntensity.Value;
            }
            else filmGrain.intensity.overrideState = false;

            if (!filmGrainResponse.IsNone)
            {
                filmGrain.response.overrideState = true;
                filmGrain.response.value = filmGrainResponse.Value;
            }
            else filmGrain.response.overrideState = false;

            if (filmGrainType == FilmGrainLookup.Custom)
            {
                if (!filmGrainTexture.IsNone)
                {
                    filmGrain.texture.overrideState = true;
                    filmGrain.texture.value = filmGrainTexture.Value;
                }
                else filmGrain.texture.overrideState = false;
            }
        }
        #endregion

        #region LENS DISTORTION()
        public void LensDistortion()
        {
            if (!volumeProfile.TryGet<LensDistortion>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<LensDistortion>(out var lensDistortion);

            lensDistortion.active = fxActive.Value;
            if (!lensDistortion.active) return;
            else

            if (!lensDistortionIntensity.IsNone)
            {
                lensDistortion.intensity.overrideState = true;
                lensDistortion.intensity.value = lensDistortionIntensity.Value;
            }
            else lensDistortion.intensity.overrideState = false;

            if (!lensDistortionXMult.IsNone)
            {
                lensDistortion.xMultiplier.overrideState = true;
                lensDistortion.xMultiplier.value = lensDistortionXMult.Value;
            }
            else lensDistortion.xMultiplier.overrideState = false;

            if (!lensDistortionYMult.IsNone)
            {
                lensDistortion.yMultiplier.overrideState = true;
                lensDistortion.yMultiplier.value = lensDistortionYMult.Value;
            }
            else lensDistortion.yMultiplier.overrideState = false;

            if (!lensDistortionCenter.IsNone)
            {
                lensDistortion.center.overrideState = true;
                lensDistortion.center.value = lensDistortionCenter.Value;
            }
            else lensDistortion.center.overrideState = false;

            if (!lensDistortionScale.IsNone)
            {
                lensDistortion.scale.overrideState = true;
                lensDistortion.scale.value = lensDistortionScale.Value;
            }
            else lensDistortion.scale.overrideState = false;
        }
        #endregion

        #region LIFT GAMMA GAIN()
        public void LiftGammaGain()
        {
            if (!volumeProfile.TryGet<LiftGammaGain>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<LiftGammaGain>(out var lgg);

            lgg.active = fxActive.Value;
            if (!lgg.active) return;
            else

            if (!lggLift.IsNone || !lggLiftIntensity.IsNone)
            {
                Vector4 lggLiftV4 = new Color(lggLift.Value.r, lggLift.Value.g, lggLift.Value.b, lggLiftIntensity.Value);
                lgg.lift.overrideState = true;
                lgg.lift.value = lggLiftV4;
            }
            else lgg.lift.overrideState = false;

            if (!lggGamma.IsNone || !lggGammaIntensity.IsNone)
            {
                Vector4 lggGammaV4 = new Color(lggGamma.Value.r, lggGamma.Value.g, lggGamma.Value.b, lggGammaIntensity.Value);
                lgg.gamma.overrideState = true;
                lgg.gamma.value = lggGammaV4;
            }
            else lgg.gamma.overrideState = false;

            if (!lggGain.IsNone || !lggGainIntensity.IsNone)
            {
                Vector4 lggGainV4 = new Color(lggGain.Value.r, lggGain.Value.g, lggGain.Value.b, lggGainIntensity.Value);
                lgg.gain.overrideState = true;
                lgg.gain.value = lggGainV4;
            }
            else lgg.gain.overrideState = false;
        }
        #endregion

        #region MOTION BLUR()
        public void MotionBlur()
        {
            if (!volumeProfile.TryGet<MotionBlur>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<MotionBlur>(out var motionBlur);

            motionBlur.active = fxActive.Value;
            if (!motionBlur.active) motionBlur.quality.overrideState = false;
            else
            {
                motionBlur.quality.overrideState = true;
                motionBlur.mode.value = motionBlurMode;
                motionBlur.quality.value = motionBlurQuality;
            }

            

            if (!motionBlurIntensity.IsNone)
            {
                motionBlur.intensity.overrideState = true;
                motionBlur.intensity.value = motionBlurIntensity.Value;
            }
            else motionBlur.intensity.overrideState = false;

            if (!motionBlurClamp.IsNone)
            {
                motionBlur.clamp.overrideState = true;
                motionBlur.clamp.value = motionBlurClamp.Value;
            }
            else motionBlur.clamp.overrideState = false;
        }
        #endregion

        #region PANINI PROJECTION()
        public void PaniniProjection()
        {
            if (!volumeProfile.TryGet<PaniniProjection>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<PaniniProjection>(out var panini);

            panini.active = fxActive.Value;
            if (!panini.active) return;
            else

            if (!paniniDistance.IsNone)
            {
                panini.distance.overrideState = true;
                panini.distance.value = paniniDistance.Value;
            }
            else panini.distance.overrideState = false;

            if (!paniniCrop.IsNone)
            {
                panini.cropToFit.overrideState = true;
                panini.cropToFit.value = paniniCrop.Value;
            }
            else panini.cropToFit.overrideState = false;
        }
        #endregion

        #region SHADOWS MIDTONES HIGHLIGHTS()
        public void ShadowsMidtonesHighlights()
        {
            if (!volumeProfile.TryGet<ShadowsMidtonesHighlights>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<ShadowsMidtonesHighlights>(out var smh);

            smh.active = fxActive.Value;
            if (!smh.active) return;
            else

            if (!smhShadows.IsNone || !smhShadowsIntensity.IsNone)
            {
                Vector4 smhShadowsV4 = new Color(smhShadows.Value.r, smhShadows.Value.g, smhShadows.Value.b, smhShadowsIntensity.Value);
                smh.shadows.overrideState = true;
                smh.shadows.value = smhShadowsV4;
            }
            else smh.shadows.overrideState = false;

            if (!smhMidtones.IsNone || !smhMidtonesIntensity.IsNone)
            {
                Vector4 smhMidtonesV4 = new Color(smhMidtones.Value.r, smhMidtones.Value.g, smhMidtones.Value.b, smhMidtonesIntensity.Value);
                smh.midtones.overrideState = true;
                smh.midtones.value = smhMidtonesV4;
            } else smh.midtones.overrideState = false;

            if (!smhHighlights.IsNone || !smhHighlightsIntensity.IsNone)
            {
                Vector4 smhHighlightsV4 = new Color(smhHighlights.Value.r, smhHighlights.Value.g, smhHighlights.Value.b, smhHighlightsIntensity.Value);
                smh.highlights.overrideState = true;
                smh.highlights.value = smhHighlightsV4;
            }
            else smh.highlights.overrideState = false;

            if (!smhShadowLimitStart.IsNone || !smhShadowLimitEnd.IsNone)
            {
                smh.shadowsStart.overrideState = true;
                smh.shadowsEnd.overrideState = true;
                smh.shadowsStart.value = smhShadowLimitStart.Value;
                if (smhShadowLimitStart.Value < 0) smhShadowLimitStart.Value = 0;

                smh.shadowsEnd.value = smhShadowLimitEnd.Value;
                if (smhShadowLimitEnd.Value < 0) smhShadowLimitEnd.Value = 0;
                if (smhShadowLimitStart.Value > smhShadowLimitEnd.Value) smhShadowLimitStart.Value = smhShadowLimitEnd.Value;
            }
            else
            {
                smh.shadowsStart.overrideState = false;
                smh.shadowsEnd.overrideState = false;
            }

            if (!smhHighlightLimitStart.IsNone || !smhHighlightLimitEnd.IsNone)
            {
                smh.highlightsStart.overrideState = true;
                smh.highlightsEnd.overrideState = true;
                smh.highlightsStart.value = smhHighlightLimitStart.Value;
                if (smhHighlightLimitStart.Value < 0) smhHighlightLimitStart.Value = 0;

                smh.highlightsEnd.value = smhHighlightLimitEnd.Value;
                if (smhHighlightLimitEnd.Value < 0) smhHighlightLimitEnd.Value = 0;
                if (smhHighlightLimitStart.Value > smhHighlightLimitEnd.Value) smhHighlightLimitStart.Value = smhHighlightLimitEnd.Value;
            }
            else
            {
                smh.highlightsStart.overrideState = false;
                smh.highlightsEnd.overrideState = false;
            }
        }
        #endregion

        #region SPLIT TONING()
        public void SplitToning()
        {
            if (!volumeProfile.TryGet<SplitToning>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<SplitToning>(out var splitToning);

            splitToning.active = fxActive.Value;
            if (!splitToning.active) return;
            else

            if (!splitToningShadows.IsNone)
            {
                splitToning.shadows.overrideState = true;
                splitToning.shadows.value = splitToningShadows.Value;
            }
            else splitToning.shadows.overrideState = false;

            if (!splitToningHighlights.IsNone)
            {
                splitToning.highlights.overrideState = true;
                splitToning.highlights.value = splitToningHighlights.Value;
            }
            else splitToning.highlights.overrideState = false;

            if (!splitToningBalance.IsNone)
            {
                splitToning.balance.overrideState = true;
                splitToning.balance.value = splitToningBalance.Value;
            }
            else splitToning.balance.overrideState = false;
        }
        #endregion

        #region VIGNETTE()
        public void Vignette()
        {
            if (!volumeProfile.TryGet<Vignette>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<Vignette>(out var vignette);

            vignette.active = fxActive.Value;
            if (!vignette.active) return;
            else

            if (!vignetteColor.IsNone)
            {
                vignette.color.overrideState = true;
                vignette.color.value = vignetteColor.Value;
            }
            else vignette.color.overrideState = false;

            if (!vignetteCenter.IsNone)
            {
                vignette.center.overrideState = true;
                vignette.center.value = vignetteCenter.Value;
            }
            else vignette.center.overrideState = false;

            if (!vignetteIntensity.IsNone)
            {
                vignette.intensity.overrideState = true;
                vignette.intensity.value = vignetteIntensity.Value;
            }
            else vignette.intensity.overrideState = false;

            if (!vignetteSmoothness.IsNone)
            {
                vignette.smoothness.overrideState = true;
                vignette.smoothness.value = vignetteSmoothness.Value;
            }
            else vignette.smoothness.overrideState = false;

            if (!vignetteRounded.IsNone)
            {
                vignette.rounded.overrideState = true;
                vignette.rounded.value = vignetteRounded.Value;
            }
            else vignette.rounded.overrideState = false;
        }
        #endregion

        #region WHITE BALANCE()
        public void WhiteBalance()
        {
            if (!volumeProfile.TryGet<WhiteBalance>(out var overridetest))
            {
                LogWarning("No " + effect + " Override found @ " + Fsm.GetFullFsmLabel(this.Fsm) + " | " + Fsm.ActiveStateName);
                Finish();
                return;
            }

            volumeProfile.TryGet<WhiteBalance>(out var wb);

            wb.active = fxActive.Value;
            if (!wb.active) return;
            else

            if (!wbTemperatere.IsNone)
            {
                wb.temperature.overrideState = true;
                wb.temperature.value = wbTemperatere.Value;
            }
            else wb.temperature.overrideState = false;

            if (!wbTint.IsNone)
            {
                wb.tint.overrideState = true;
                wb.tint.value = wbTint.Value;
            }
            else wb.tint.overrideState = false;
        }
        #endregion

        //Defaults

        #region SetDefault()
        void SetDefault(volumeOverrides fx)
        {
            if (fx == volumeOverrides.None) return;
            
            if (fx == volumeOverrides.Bloom)
            {
                bloomThreshold = 0.9f;
                bloomIntensity = 0;
                bloomScatter = 0.7f;
                bloomTint = new Color(1, 1, 1, 1);
                bloomClamp = 65472f;
                bloomHQFiltering = false;
                bloomSkipIterations = 1;
                bloomDirtTexture = null;
                bloomDirtIntensity = 0f;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.ChannelMixer)
            {
                redRed = 100f;
                redGreen = 0f;
                redBlue = 0f;
                greenRed = 0;
                greenGreen = 100;
                greendBlue = 0;
                blueRed = 0;
                blueGreen = 0;
                blueBlue = 100;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.ChromaticAberration)
            {
                chromaticAberrationIntensity = 1;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.ColorAdjustments)
            {
                colorAdjustmentsExposure = 0;
                colorAdjustmentsContrast = 0;
                colorAdjustmentsFilter = new Color(1, 1, 1, 1);
                colorAdjustmentsHueShift = 0;
                colorAdjustmentsSaturation = 0;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.ColorLookup)
            {
                colorLookuplookupTexture = null;
                colorLookupContribution = 1f;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.DepthOfField)
            {
                depthMode = DepthOfFieldMode.Off;
                gaussianStart = 10;
                gaussianEnd = 30;
                gaussianMaxRadius = 1;
                gaussianHQSampling = false;
                bokehFocusDistance = 10;
                bokehFocalLenght = 50;
                bokehAperture = 5.6f;
                bokehBladeCount = 5;
                bokehBladeCurvature = 1;
                bokehBladeRotation = 0;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.FilmGrain)
            {
                filmGrainType = FilmGrainLookup.Thin1;
                filmGrainIntensity = 0;
                filmGrainResponse = 0.8f;
                filmGrainTexture = null;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.LensDistortion)
            {
                lensDistortionIntensity = 0;
                lensDistortionXMult = 1;
                lensDistortionYMult = 1;
                lensDistortionCenter = new Vector2(0.5f, 0.5f);
                lensDistortionScale = 1;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.LiftGammaGain)
            {
                lggLift = new Color(1, 1, 1, 0);
                lggLiftIntensity = 0;
                lggGamma = new Color(1, 1, 1, 0);
                lggGammaIntensity = 0;
                lggGain = new Color(1, 1, 1, 0);
                lggGainIntensity = 0;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.MotionBlur)
            {
                motionBlurMode = MotionBlurMode.CameraAndObjects;
                motionBlurQuality = MotionBlurQuality.Low;
                motionBlurIntensity = 0f;
                motionBlurClamp = 0.05f;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.PaniniProjection)
            {
                paniniDistance = 0;
                paniniCrop = 1;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.ShadowsMidtonesHighlights)
            {
                smhShadows = new Color(1, 1, 1, 0);
                smhShadowsIntensity = 0;
                smhMidtones = new Color(1, 1, 1, 0);
                smhMidtonesIntensity = 0;
                smhHighlights = new Color(1, 1, 1, 0);
                smhHighlightsIntensity = 0;
                smhShadowLimitStart = 0;
                smhShadowLimitEnd = 0.3f;
                smhHighlightLimitStart = 0.55f;
                smhHighlightLimitEnd = 1;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.SplitToning)
            {
                splitToningShadows = new Color(0.5f, 0.5f, 0.5f, 1);
                splitToningHighlights = new Color(0.5f, 0.5f, 0.5f, 1);
                splitToningBalance = 0;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.Vignette)
            {
                vignetteColor = new Color(0, 0, 0, 1);
                vignetteCenter = new Vector2(0.5f, 0.5f);
                vignetteIntensity = 0;
                vignetteSmoothness = 0.2f;
                vignetteRounded = false;
                setDefault = false;
                return;
            }

            if (fx == volumeOverrides.Vignette)
            {
                wbTemperatere = 0;
                wbTint = 0;
                setDefault = false;
                return;
            }
        }
        #endregion
    }
}